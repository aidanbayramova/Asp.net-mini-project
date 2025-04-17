using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.Blog
{
    public class BlogDetailsBannerViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
