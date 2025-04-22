using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Category;
using Asp.net_mini_project.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Mvc;
using static Asp.net_mini_project.ViewComponents.Home.ProductViewComponent;

namespace Asp.net_mini_project.ViewComponents.Home
{
    public class BlogViewComponent : ViewComponent
    {
        private readonly IBlogService _blogService;
        public BlogViewComponent(IBlogService blogService)
        {
            _blogService = blogService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var blogList = await _blogService.GetAllAsync();
            return View(blogList);
        }

    }
}
