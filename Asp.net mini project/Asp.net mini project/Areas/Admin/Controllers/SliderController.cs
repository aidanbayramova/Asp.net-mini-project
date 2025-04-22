using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Brand;
using Asp.net_mini_project.ViewModels.Admin.Slider;
using FiorelloBackendPB103.Helpers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly ISliderService _sliderService;
        private readonly IWebHostEnvironment _env;
        public SliderController(ISliderService sliderService,
                                IWebHostEnvironment env)
        {
            _sliderService = sliderService;
            _env = env;
        }
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var sliders = await _sliderService.GetAllAsync();
            var sliderS = sliders.Select(m => new SliderVM { Id = m.Id, Image = m.Img });
            return View(sliderS);
        }
  
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
                return View(request);

            if (request.Image.CheckFilesSize(200))
            {
                ModelState.AddModelError("Image", "Image size must be max 200KB");
                return View(request);
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Image type must be image format");
                return View(request);
            }


            await _sliderService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var slider = await _sliderService.GetByIdAsync((int)id);
            if (slider == null) return NotFound();

            await _sliderService.DeleteAsync(slider);

            return RedirectToAction(nameof(Index));
        }
       
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var slider = await _sliderService.GetByIdAsync(id);
            if (slider == null) return NotFound();

            var model = new SliderEditVM
            {
                Id = slider.Id,
                Img = slider.Img
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(SliderEditVM sliderEditVM)
        {
            //if (!ModelState.IsValid) return View(brandEditVM);

            await _sliderService.EditAsync(sliderEditVM);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _sliderService.GetByIdAsync(id.Value);
            if (slider == null) return NotFound();

            var detailVM = new SliderDetailVM
            {
                Img = slider.Img
            };

            return View(detailVM);
        }


    }
}

