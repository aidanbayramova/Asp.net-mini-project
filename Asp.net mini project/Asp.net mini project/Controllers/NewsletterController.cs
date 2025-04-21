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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Subscribe(string email)
        {

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
            {
                TempData["Error"] = "Please enter a valid email address.";
                return RedirectToAction("Index", "Home");
            }

            bool emailExists = await _newsletterService.CheckEmailExistsAsync(email);
            if (emailExists)
            {
                TempData["Error"] = "You've already subscribed with this email.";
                return RedirectToAction("Index", "Home");
            }

            await _newsletterService.AddAsync(email);
            TempData["Success"] = "You have successfully subscribed!";
            return RedirectToAction("Index", "Home");
        }

    }
}

