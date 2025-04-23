using Asp.net_mini_project.Data;
using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Asp.net_mini_project.Controllers
{
    public class BasketController : Controller
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IProductService _productService;

        public BasketController(IHttpContextAccessor accessor,
                              IProductService productService)
        {
            _accessor = accessor;
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<BasketVM> basketDatas = [];
            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }

            List<BasketProductVM> products = new List<BasketProductVM>();
            foreach (var item in basketDatas)
            {
                var allProducts = await _productService.GetByIdAsync(item.ProductId);
                products.Add(new BasketProductVM
                {
                    Count = item.ProductCount,                   
                    Id = allProducts.Id,
                    
                    Name = allProducts.Name,
                    Price = allProducts.Price,
                    Img = allProducts.ProductImgs.FirstOrDefault(x => x.IsMain).Img,
                    CategoryName = allProducts.Category.Name,
                    Product = allProducts,
                });
            }
            ViewBag.totalPrice = products.Sum(x => x.Price * x.Count);
            return View(products);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            List<BasketVM> basketDatas = [];

            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }

            var existBasketData = basketDatas.FirstOrDefault(m => m.ProductId == id);

            if (existBasketData != null)
            {
                basketDatas.Remove(existBasketData);
            }

            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketDatas));

            int count = basketDatas.Sum(m => m.ProductCount);

            List<BasketProductVM> products = new();

            foreach (var item in basketDatas)
            {
                var product = await _productService.GetByIdAsync(item.ProductId);
                products.Add(new BasketProductVM
                {
                    Product = product,
                    Count = item.ProductCount,

                });
            }

            decimal total = products.Sum(m => m.Product.Price * m.Count);
            return Ok(new { total, count });
        }
    }
    
}
