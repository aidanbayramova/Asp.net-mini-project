using Asp.net_mini_project.Data;
using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Category;
using FiorelloBackendPB103.Areas.Admin.ViewModels.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var categories = await _categoryService.GetAllAsync();
            var result = categories.Select(m => new CategoryVM { Id = m.Id, Name = m.Name });

            return View(result);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            try
            {
                await _categoryService.CreateAsync(request);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var category = await _categoryService.GetByIdAsync((int)id);
            if (category == null)
            {
                return NotFound();
            }

            try
            {
                await _categoryService.DeleteAsync((int)id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return BadRequest();

            var category = await _categoryService.GetByIdAsync((int)id);
            if (category == null) return NotFound();

            var viewModel = new CategoryDetailVM
            {
                Name = category.Name,
                ProductCount = category.Products?.Count ?? 0,
                CreateDate = category.CreateDate
            };

            return View(viewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _categoryService.GetByIdAsync((int)id);
            if (category == null) return NotFound();

            var viewModel = new CategoryEditVM { Id = category.Id, Name = category.Name };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryEditVM request)
        {
            if (!ModelState.IsValid) return View(request);

            try
            {
                await _categoryService.EditAsync(request); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(request);
            }

            return RedirectToAction(nameof(Index));
        }


    }


}