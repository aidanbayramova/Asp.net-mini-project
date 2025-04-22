using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.About;
using Asp.net_mini_project.ViewModels.Admin.Advertisement;
using FiorelloBackendPB103.Helpers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutService.GetAllAsync();
            return View(abouts);
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var about = await _aboutService.GetDetailAsync(id);
            if (about == null) return NotFound();

            return View(about);
        }
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(AboutCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _aboutService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var about = await _aboutService.GetByIdAsync(id);
            if (about == null) return NotFound();

            var model = new AboutEditVM
            {
                Id = id,
                Title = about.Title,
                Description = about.Description,
                VideoUrl = about.VideoUrl,
                Img = about.Image,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id, AboutEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _aboutService.EditAsync(id, model);

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var about = await _aboutService.GetByIdAsync(id);
            if (about == null) return NotFound();


            await _aboutService.DeleteAsync(about);

            return RedirectToAction(nameof(Index));
        }
    }
}

