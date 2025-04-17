using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.About
{
    public class AboutViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
