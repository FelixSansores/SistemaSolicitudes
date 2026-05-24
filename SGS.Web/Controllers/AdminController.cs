using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SGS.Class;
using SGS.Class.Enums;

namespace SGS.Web.Controllers
{
    /// <summary>
    /// Controlador encargado de la administración general del sistema.
    /// Permite visualizar estadísticas e indicadores principales.
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly SGSDb _context;

        /// <summary>
        /// Inicializa una nueva instancia del controlador AdminController.
        /// </summary>
        /// <param name="context">
        /// Contexto de base de datos del sistema.
        /// </param>
        public AdminController(SGSDb context)
        {
            _context = context;
        }

        /// <summary>
        /// Muestra el panel principal de administración con estadísticas
        /// de solicitudes registradas en el sistema.
        /// </summary>
        /// <returns>
        /// Vista del dashboard administrativo.
        /// </returns>
        public IActionResult Index()
        {
            ViewBag.TotalRequests =
                _context.Requests.Count();

            ViewBag.PendingRequests =
                _context.Requests.Count(r =>
                    r.Status == RequestStatus.Pending);

            ViewBag.InProgressRequests =
                _context.Requests.Count(r =>
                    r.Status == RequestStatus.InProgress);

            ViewBag.CompletedRequests =
                _context.Requests.Count(r =>
                    r.Status == RequestStatus.Completed);

            ViewBag.UrgentRequests =
                _context.Requests.Count(r =>
                    r.Priority == RequestPriority.Urgent);

            return View();
        }
    }
}