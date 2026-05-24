using Microsoft.AspNetCore.Mvc;
using SGS.Class.Enums;
using SGS.Class.Interfaces;
using SGS.Class.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using SGS.Class;
using Microsoft.AspNetCore.Identity;

namespace SGS.Web.Controllers
{
    /// <summary>
    /// Controlador encargado de gestionar las solicitudes del sistema.
    /// Permite crear, consultar, editar, eliminar y comentar solicitudes.
    /// </summary>
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly SGSDb _context;
        private readonly IRequestService _service;
        private readonly ICommentService _commentService;
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Inicializa una nueva instancia del controlador RequestsController.
        /// </summary>
        /// <param name="service">Servicio de solicitudes.</param>
        /// <param name="commentService">Servicio de comentarios.</param>
        /// <param name="context">Contexto de base de datos.</param>
        /// <param name="userManager">Administrador de usuarios.</param>
        public RequestsController(
            IRequestService service,
            ICommentService commentService,
            SGSDb context,
            UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _commentService = commentService;
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Muestra la lista de solicitudes aplicando filtros por estado,
        /// prioridad, categoría y técnico asignado.
        /// </summary>
        /// <param name="status">Estado de la solicitud.</param>
        /// <param name="priority">Prioridad de la solicitud.</param>
        /// <param name="categoryId">Identificador de la categoría.</param>
        /// <param name="assignedUserId">Identificador del técnico asignado.</param>
        /// <returns>
        /// Vista con la lista de solicitudes filtradas.
        /// </returns>
        public async Task<IActionResult> Index(
            RequestStatus? status,
            RequestPriority? priority,
            int? categoryId,
            string? assignedUserId)
        {
            ViewBag.Categories = new SelectList(
                _context.Categories.ToList(),
                "Id",
                "Name");

            var tecnicos = await _userManager.GetUsersInRoleAsync("Tecnico");

            ViewBag.Technicians = new SelectList(
                    tecnicos,
                    "Id",
                    "FullName");

            var requests = await _service.GetFilteredAsync(
                    status,
                    priority,
                    categoryId,
                    assignedUserId);

            var currentUserId = _userManager.GetUserId(User);

            if (User.IsInRole("Tecnico"))
            {
                requests = requests
                    .Where(r => r.AssignedUserId == currentUserId)
                    .ToList();
            }
            else if (User.IsInRole("User"))
            {
                requests = requests
                    .Where(r => r.CreatedByUserId == currentUserId)
                    .ToList();
            }

            ViewBag.SelectedStatus = status;
            ViewBag.SelectedPriority = priority;
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.SelectedAssignedUserId = assignedUserId;

            return View(requests);
        }

        /// <summary>
        /// Muestra el formulario para crear una nueva solicitud.
        /// </summary>
        /// <returns>
        /// Vista del formulario de creación.
        /// </returns>
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(
                _context.Categories.ToList(),
                "Id",
                "Name");

            return View();
        }

        /// <summary>
        /// Crea una nueva solicitud en el sistema.
        /// </summary>
        /// <param name="request">
        /// Información de la solicitud a registrar.
        /// </param>
        /// <returns>
        /// Redirección al listado de solicitudes.
        /// </returns>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(RequestModel request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(
                    _context.Categories.ToList(),
                    "Id",
                    "Name");

                return View(request);
            }

            request.CreatedByUserId = _userManager.GetUserId(User);
            request.CreatedAt = DateTime.Now;
            request.Status = RequestStatus.Pending;

            await _service.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Muestra el detalle de una solicitud junto con sus comentarios.
        /// </summary>
        /// <param name="id">
        /// Identificador de la solicitud.
        /// </param>
        /// <returns>
        /// Vista con la información detallada de la solicitud.
        /// </returns>
        public async Task<IActionResult> Details(int id)
        {
            var request = await _service.GetWithCommentsAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        /// <summary>
        /// Muestra el formulario para editar una solicitud existente.
        /// </summary>
        /// <param name="id">
        /// Identificador de la solicitud.
        /// </param>
        /// <returns>
        /// Vista de edición de la solicitud.
        /// </returns>
        [Authorize(Roles = "Admin,Tecnico")]
        public async Task<IActionResult> Edit(int id)
        {
            var request = await _service.GetByIdAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(
                _context.Categories.ToList(),
                "Id",
                "Name",
                request.CategoryId);

            var tecnicos = await _userManager.GetUsersInRoleAsync("Tecnico");

            ViewBag.Technicians = new SelectList(
                tecnicos,
                "Id",
                "FullName",
                request.AssignedUserId);

            return View(request);
        }

        /// <summary>
        /// Actualiza la información de una solicitud existente.
        /// </summary>
        /// <param name="request">
        /// Datos actualizados de la solicitud.
        /// </param>
        /// <returns>
        /// Redirección al listado de solicitudes.
        /// </returns>
        [Authorize(Roles = "Admin,Tecnico")]
        [HttpPost]
        public async Task<IActionResult> Edit(RequestModel request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(
                    _context.Categories.ToList(),
                    "Id",
                    "Name",
                    request.CategoryId);

                var tecnicos = await _userManager.GetUsersInRoleAsync("Tecnico");

                ViewBag.Technicians = new SelectList(
                    tecnicos,
                    "Id",
                    "FullName",
                    request.AssignedUserId);

                return View(request);
            }

            var existingRequest = await _service.GetByIdAsync(request.Id);

            if (existingRequest == null)
            {
                return NotFound();
            }

            existingRequest.Title = request.Title;
            existingRequest.Description = request.Description;
            existingRequest.Location = request.Location;
            existingRequest.CategoryId = request.CategoryId;
            existingRequest.Status = request.Status;
            existingRequest.Priority = request.Priority;
            existingRequest.AssignedUserId = request.AssignedUserId;

            if (request.Status == SGS.Class.Enums.RequestStatus.Completed)
            {
                existingRequest.ClosedAt = DateTime.Now;
            }
            else
            {
                existingRequest.ClosedAt = null;
            }

            await _service.UpdateAsync(existingRequest);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Muestra la vista de confirmación para eliminar una solicitud.
        /// </summary>
        /// <param name="id">
        /// Identificador de la solicitud.
        /// </param>
        /// <returns>
        /// Vista de confirmación de eliminación.
        /// </returns>
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _service.GetByIdAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        /// <summary>
        /// Elimina una solicitud del sistema.
        /// </summary>
        /// <param name="id">
        /// Identificador de la solicitud.
        /// </param>
        /// <returns>
        /// Redirección al listado de solicitudes.
        /// </returns>
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Agrega un comentario a una solicitud.
        /// </summary>
        /// <param name="comment">
        /// Información del comentario a registrar.
        /// </param>
        /// <returns>
        /// Redirección a la vista de detalles de la solicitud.
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentModel comment)
        {
            if (string.IsNullOrWhiteSpace(comment.Content))
            {
                return RedirectToAction(
                    nameof(Details),
                    new { id = comment.RequestId });
            }

            comment.CreatedAt = DateTime.Now;

            await _commentService.CreateAsync(comment);

            return RedirectToAction(
                nameof(Details),
                new { id = comment.RequestId });
        }
    }
}