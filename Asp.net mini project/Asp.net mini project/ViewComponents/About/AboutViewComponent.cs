using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Brand;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.About
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly IAboutService _aboutService;
        public AboutViewComponent(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var abouts = await _aboutService.GetAllAsync();
            return View(abouts);
        }
    }
}
