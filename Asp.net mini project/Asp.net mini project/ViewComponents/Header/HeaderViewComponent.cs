using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.Header
{
    public class HeaderViewComponent :ViewComponent
    {
       public async Task<IViewComponentResult> InvokeAsync()
       {
            return await Task.FromResult(View());
       }
    }
}
