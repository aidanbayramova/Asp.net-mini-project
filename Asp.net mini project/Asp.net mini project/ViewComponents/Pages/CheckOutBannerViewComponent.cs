using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.Pages
{
    public class CheckOutBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
