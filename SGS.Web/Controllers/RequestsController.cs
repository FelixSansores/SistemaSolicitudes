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
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly SGSDb _context;
        private readonly IRequestService _service;
        private readonly ICommentService _commentService;
        private readonly UserManager<ApplicationUser> _userManager;

        public RequestsController(IRequestService service, ICommentService commentService, SGSDb context, 
            UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _commentService = commentService;
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var requests = await _service.GetAllAsync();

            return View(requests);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(RequestModel request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
                return View(request);
            }
            await _service.CreateAsync(request);
            request.CreatedAt = DateTime.Now;

            request.Status = RequestStatus.Pending;

            request.Priority = RequestPriority.Medium;
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var request = await _service.GetWithCommentsAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }
        [Authorize(Roles = "Admin, Tecnico")]
        public async Task<IActionResult> Edit(int id)
        {
            var request = await _service.GetByIdAsync(id);

            if (request == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", request.CategoryId);
            var tecnicos = await _userManager.GetUsersInRoleAsync("Tecnico");
            ViewBag.Technicians = new SelectList(tecnicos, "Id", "FullName", request.AssignedUserId);
            return View(request);
        }

        [Authorize(Roles = "Admin, Tecnico")]
        [HttpPost]
        public async Task<IActionResult> Edit(RequestModel request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", request.CategoryId);
                var tecnicos = await _userManager.GetUsersInRoleAsync("Tecnico");
                ViewBag.Technicians = new SelectList(tecnicos, "Id", "FullName", request.AssignedUserId);
                return View(request);
            }

            if (request.Status == SGS.Class.Enums.RequestStatus.Completed)
            {
                request.ClosedAt = DateTime.Now;
            }
            else
            {
                request.ClosedAt = null;
            }
            await _service.UpdateAsync(request);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            var request = await _service.GetByIdAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentModel comment)
        {
            if (string.IsNullOrWhiteSpace(comment.Content))
            {
                return RedirectToAction(nameof(Details), new { id = comment.RequestId });
            }

            comment.CreatedAt = DateTime.Now;

            await _commentService.CreateAsync(comment);

            return RedirectToAction(nameof(Details), new { id = comment.RequestId });
        }

    }
}