using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using SGS.Class.Enums;
using SGS.Class.Interfaces;
using SGS.Class.Models;
using Microsoft.AspNetCore.Authorization;

namespace SGS.Web.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly IRequestService _service;
        private readonly ICommentService _commentService;

        public RequestsController(IRequestService service, ICommentService commentService)
        {
            _service = service;

            _commentService = commentService;
        }
        public async Task<IActionResult> Index()
        {
            var requests = await _service.GetAllAsync();

            return View(requests);
        }

        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(RequestModel request)
        {
            if (!ModelState.IsValid)
            {
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
        [Authorize(Roles = "Admin,Tecnico")]
        public async Task<IActionResult> Edit(int id)
        {
            var request = await _service.GetByIdAsync(id);

            if (request == null)
            {
                return NotFound();
            }

            return View(request);
        }

        [Authorize(Roles = "Admin,Tecnico")]
        [HttpPost]
        public async Task<IActionResult> Edit(RequestModel request)
        {
            if (!ModelState.IsValid)
            {
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
            comment.CreatedAt = DateTime.Now;

            await _commentService.CreateAsync(comment);

            return RedirectToAction(nameof(Details),
                new { id = comment.RequestId });
        }

    }
}