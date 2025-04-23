using System.Diagnostics;
using Asp.net_mini_project.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Asp.net_mini_project.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHttpContextAccessor _accessor;
        public HomeController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProductToBasket(int id, int quantity)
        {

            List<BasketVM> basketDatas = [];
            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }

            var existBasketData = basketDatas.FirstOrDefault(x => x.ProductId == id);
            if (existBasketData != null)
            {
                existBasketData.ProductCount += quantity;
            }
            else
            {
                basketDatas.Add(new BasketVM { ProductId = id, ProductCount = quantity });
            }
            _accessor.HttpContext.Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketDatas));
            return Ok(basketDatas.Sum(x => x.ProductCount));
        }



    }
}
