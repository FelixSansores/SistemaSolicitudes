using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SGS.Class;
using SGS.Class.Enums;

namespace SGS.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly SGSDb _context;
        public AdminController(SGSDb context)
        {
            _context = context;
        }
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