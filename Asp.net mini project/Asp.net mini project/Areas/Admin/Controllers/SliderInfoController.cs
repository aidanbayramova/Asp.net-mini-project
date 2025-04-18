using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.SliderInfo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderInfoController : Controller
    {
        private readonly ISliderInfoService _sliderInfoService;
        private readonly IWebHostEnvironment _env;
        public SliderInfoController(ISliderInfoService sliderInfoService,
                                IWebHostEnvironment env)
        {
            _sliderInfoService = sliderInfoService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var sliderInfos = await _sliderInfoService.GetAllAsync();

            var vmList = sliderInfos.Select(s => new SliderInfoVM
            {
                Id = s.Id,
                Title = s.Title,
                Discount = s.Discount,
                Description = s.Description
            });

            return View(vmList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderInfoCreateVM request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _sliderInfoService.CreateAsync(request);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            var sliderInfo = await _sliderInfoService.GetByIdAsync((id.Value));
            if (sliderInfo == null) return NotFound();

            await _sliderInfoService.DeleteAsync(sliderInfo);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var sliderInfo = await _sliderInfoService.GetByIdAsync(id);
            if (sliderInfo == null) return NotFound();

            var model = new SliderInfoEditVM
            {
                Id = sliderInfo.Id,
                Title = sliderInfo.Title,
                Discount = sliderInfo.Discount,
                Description = sliderInfo.Description
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SliderInfoEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _sliderInfoService.EditAsync(model);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var sliderInfo = await _sliderInfoService.GetByIdAsync(id.Value);
            if (sliderInfo == null) return NotFound();

            var detailVM = new SliderInfoDetailVM
            {
                Title = sliderInfo.Title,
                Discount = sliderInfo.Discount,
                Description = sliderInfo.Description
            };

            return View(detailVM);
        }


    }
}
