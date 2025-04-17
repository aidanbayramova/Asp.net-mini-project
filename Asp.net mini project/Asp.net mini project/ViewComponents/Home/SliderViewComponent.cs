using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.Home
{
    public class SliderViewComponent:ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
