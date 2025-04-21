using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Category;
using Asp.net_mini_project.ViewModels.Admin.Product;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.Home
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductViewComponent(ICategoryService categoryService,
                                     IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<ProductVM> products = await _productService.GetAllAsync();
            IEnumerable<CategoryVM> categories = await _categoryService.GetAllAsync();
            return await Task.FromResult(View(new ProductVMVC { Categories = categories, Products = products }));
        }
        public class ProductVMVC
        {
            public IEnumerable<ProductVM> Products { get; set; }
            public IEnumerable<CategoryVM> Categories { get; set; }
        }
    }
}
