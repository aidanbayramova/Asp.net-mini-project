using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.Controllers
{
    public class NewsletterController : Controller
    {
        private readonly INewsletterService _newsletterService;

        public NewsletterController(INewsletterService newsletterService)
        {
            _newsletterService = newsletterService;
        }
        public IActionResult Index()
        {
            return View(new Newsletter());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                return Json(new { success = false, message = "Please enter a valid email address." });
            }

            bool emailExists = await _newsletterService.CheckEmailExistsAsync(email);
            if (emailExists)
            {
                return Json(new { success = false, message = "You've already subscribed with this email." });
            }

            await _newsletterService.AddAsync(email);

            return Json(new { success = true, message = "You have successfully subscribed!" });
        }


    }
}

