using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.Home
{
    public class BannerViewComponent : ViewComponent
    {
        private readonly IAdvertisementService _advertisementService;
        public BannerViewComponent(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Advertisement> advertisements = await _advertisementService.GetAllAsync();
            return await Task.FromResult(View(advertisements));
        }
    }
}
