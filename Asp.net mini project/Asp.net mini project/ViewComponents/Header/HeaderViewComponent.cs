using Asp.net_mini_project.ViewModels.UI;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Asp.net_mini_project.ViewComponents.Header
{
    public class HeaderViewComponent :ViewComponent
    {
        private readonly IHttpContextAccessor _accessor;
        public HeaderViewComponent(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<BasketVM> basketDatas = [];
            if (_accessor.HttpContext.Request.Cookies["basket"] != null)
            {
                basketDatas = JsonConvert.DeserializeObject<List<BasketVM>>(_accessor.HttpContext.Request.Cookies["basket"]);
            }
            int count = basketDatas.Sum(x => x.ProductCount);
            return await Task.FromResult(View(new HeaderVM { ProductCountOfBasket = count }));
        }
    }
}
