using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Advertisement;
using Asp.net_mini_project.ViewModels.Admin.Blog;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{

        [Area("Admin")]
        public class BlogController : Controller
        {
            private readonly IBlogService _blogService;
            private readonly IWebHostEnvironment _webHostEnv;

            public BlogController(IBlogService blogService, IWebHostEnvironment webHostEnv)
            {
                _blogService = blogService;
                _webHostEnv = webHostEnv;
            }

            [HttpGet]
            public async Task<IActionResult> Index()
            {
                var blogList = await _blogService.GetAllAsync();
                return View(blogList);
            }

            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(BlogCreateVM createVM)
            {
                await _blogService.CreateAsync(createVM);
                return RedirectToAction(nameof(Index));
            }

            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                var editModel = await _blogService.GetEditModelAsync(id);
                if (editModel == null) return NotFound();

                return View(editModel);
            }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogEditVM updateVM)
        {
            if (!ModelState.IsValid) return View(updateVM);

            await _blogService.EditAsync(updateVM);
            return RedirectToAction("Index");
        }

        [HttpGet]
            public async Task<IActionResult> Detail(int id)
            {
                var detailVM = await _blogService.GetDetailAsync(id);
                if (detailVM == null) return NotFound();

                return View(detailVM);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Delete(int id)
            {
                var blogToDelete = await _blogService.GetByIdAsync(id);
                if (blogToDelete == null) return NotFound();

                await _blogService.DeleteAsync(blogToDelete);
                return RedirectToAction(nameof(Index));
            }

            [HttpPost]
            public async Task<IActionResult> DeleteImg(int imageId, int blogId)
            {
                await _blogService.DeleteImgAsync(imageId);
                return RedirectToAction("Edit", new { id = blogId });
            }
        }
 }



