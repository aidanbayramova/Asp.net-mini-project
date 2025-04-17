using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.Contact
{
    public class ContactSectionMapViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
