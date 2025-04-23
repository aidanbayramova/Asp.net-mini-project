using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Newsletter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NewsletterController : Controller
    {
        private readonly INewsletterService _newsletterService;

        public NewsletterController(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var newsletters = await _newsletterService.GetAllAsync();

            var viewModel = new NewsletterVM
            {
                Newsletters = newsletters.Select(n => new NewsletterVM
                {
                    Id = n.Id,
                    Email = n.Email,
                    CreateDate = n.CreateDate
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _newsletterService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
