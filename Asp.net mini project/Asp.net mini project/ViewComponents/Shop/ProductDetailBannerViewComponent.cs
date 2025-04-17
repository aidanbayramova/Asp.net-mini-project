using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.Shop
{
    public class ProductDetailBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
