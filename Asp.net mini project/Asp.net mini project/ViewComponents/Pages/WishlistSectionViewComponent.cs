using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.Wishlist
{
    public class WishlistSectionViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return await Task.FromResult(View());
        }
    }
}
