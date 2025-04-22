using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductService productService, 
                                 ICategoryService categoryService,
                                 IWebHostEnvironment env)
        {
            _productService = productService;
            _categoryService = categoryService;
            _env = env;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories == null)
            {
                
                return View("Error"); 
            }
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", model.CategoryId);
                return View(model);
            }

           var categoryExist = await _categoryService.GetByIdAsync(model.CategoryId);
            if (categoryExist == null)
            {
                ModelState.AddModelError("CategoryId", "Invalid category selected.");
                ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", model.CategoryId);
                return View(model);
            }           
            await _productService.CreateAsync(model);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", product.CategoryId);
            var model = new ProductEditVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Desc = product.Desc,
                CategoryId = product.CategoryId,
                
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Edit(ProductEditVM model)
        {
            if (ModelState.IsValid)
            {
                await _productService.EditAsync(model);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(await _categoryService.GetAllAsync(), "Id", "Name", model.CategoryId);
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _productService.GetDetailAsync(id);
            if (product == null) return NotFound();

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound();

            await _productService.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteImage(int imageId, int productId)
        {
            await _productService.DeleteImageAsync(imageId);
            return RedirectToAction("Edit", new { id = productId });
        }

    }
}
