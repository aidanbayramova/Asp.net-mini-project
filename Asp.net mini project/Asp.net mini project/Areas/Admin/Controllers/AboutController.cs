using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.About;
using Asp.net_mini_project.ViewModels.Admin.Advertisement;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;
        private readonly IWebHostEnvironment _env;
        public AboutController(IAboutService aboutService,
                                IWebHostEnvironment env)
        {
            _aboutService = aboutService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var about = await _aboutService.GetAllAsync();

            //var aboutSection = about.Select(m => new AboutVM
            //{
            //    Description = m.Description,
            //    VideoFile = m.Video,
            //    Title = m.Title
            //});


            return View();
        }
    }
}
