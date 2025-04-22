using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Brand;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.About
{
    public class ClientViewComponent : ViewComponent
    {
        private readonly IBrandService _brandService;
        public ClientViewComponent(IBrandService brandService)
        {
            _brandService = brandService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brand = await _brandService.GetAllAsync();

            var model = brand.Select(m => new BrandVM
            {
                Id = m.Id,
                Img = m.Img,

            });

            return View(model);
        }
    }
}
