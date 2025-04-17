using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.UnderConstruction
{
    public class UnderConstructionViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
