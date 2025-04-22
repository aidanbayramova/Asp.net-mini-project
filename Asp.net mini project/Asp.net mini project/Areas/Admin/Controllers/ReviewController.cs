using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{
  
        [Area("Admin")]

        public class ReviewController : Controller
        {
            private readonly IReviewService _reviewService;

            public ReviewController(IReviewService reviewService)
            {
                _reviewService = reviewService;
            }
              [Authorize(Roles = "Admin,SuperAdmin")]
            public async Task<IActionResult> Index()
            {
                var reviews = await _reviewService.GetAllAsync();
                return View(reviews);
            }
             [Authorize(Roles = "Admin,SuperAdmin")]
            public async Task<IActionResult> Details(int id)
            {
                var review = await _reviewService.GetByIdAsync(id);
                if (review == null) return NotFound();
                return View(review);
            }
             [Authorize(Roles = "SuperAdmin")]
            public async Task<IActionResult> Create()
            {

                var model = await _reviewService.GetCreateModelAsync();
                return View(model);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(ReviewCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Console.WriteLine($"ConsumerId: {model.Id}");

           
            if (model.Id == null)
            {
                ModelState.AddModelError("ConsumerId", "ConsumerId is required.");
                return View(model);
            }

            await _reviewService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
                var model = await _reviewService.GetEditModelAsync(id);
                if (model == null) return NotFound();
                return View(model);
        }

            [HttpPost]
            [ValidateAntiForgeryToken]
            [Authorize(Roles = "SuperAdmin")]
            public async Task<IActionResult> Edit(ReviewEditVM model)
            {
                if (!ModelState.IsValid)
                    return View(model);
                await _reviewService.EditAsync(model);
                return RedirectToAction(nameof(Index));
            }
             [Authorize(Roles = "SuperAdmin")]
            public async Task<IActionResult> Delete(int id)
            {
                var review = new Review { Id = id };
                await _reviewService.DeleteAsync(review);
                return RedirectToAction(nameof(Index));
            }
        }
    
}