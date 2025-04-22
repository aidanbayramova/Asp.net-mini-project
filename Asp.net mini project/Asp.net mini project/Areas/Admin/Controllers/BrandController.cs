using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Advertisement;
using Asp.net_mini_project.ViewModels.Admin.Brand;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IActionResult> Index()
        {
            var brand = await _brandService.GetAllAsync();

            var model = brand.Select(m => new BrandVM
            {
                Id = m.Id,
                Img = m.Img,
              
            });

            return View(model);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null) return NotFound();

            var viewModel = new BrandDetailVM
            {
                Id = brand.Id,
                Img = brand.Img
            };

            return View(viewModel);
        }


        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BrandCreateVM brandCreateVM)
        {
            if (!ModelState.IsValid) return View();

            await _brandService.CreateAsync(brandCreateVM);
            return RedirectToAction(nameof(Index));
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null) return NotFound();

            var model = new BrandEditVM
            {
                Id = brand.Id,
                Img = brand.Img
            };

            return View(model);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BrandEditVM brandEditVM)
        {
            //if (!ModelState.IsValid) return View(brandEditVM);

            await _brandService.EditAsync(brandEditVM);
            return RedirectToAction(nameof(Index));
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _brandService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
