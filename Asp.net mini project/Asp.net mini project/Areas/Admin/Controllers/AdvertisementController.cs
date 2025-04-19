using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Advertisement;
using Asp.net_mini_project.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IWebHostEnvironment _env;
        public AdvertisementController(IAdvertisementService advertisementService,
                                IWebHostEnvironment env)
        {
            _advertisementService    = advertisementService;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var advertisement = await _advertisementService.GetAllAsync();

            var advertisementI = advertisement.Select(m => new AdvertisementVM
            {
                Id = m.Id,
                Img = m.Img,
                Title = m.Title
            });

            return View(advertisementI); 
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdvertisementCreateVM request)
        {
            if (!ModelState.IsValid)
                return View(request);

            await _advertisementService.CreateAsync(request);

            return RedirectToAction(nameof(Index)); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest(); 

            var advertisement = await _advertisementService.GetByIdAsync(id.Value); 
            if (advertisement == null) return NotFound(); 

            await _advertisementService.DeleteAsync(advertisement); 

            return RedirectToAction(nameof(Index)); 
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var advertisement = await _advertisementService.GetByIdAsync(id);
            if (advertisement == null) return NotFound();

            var model = new AdvertisementEditVM
            {
                Id = advertisement.Id,
                Title = advertisement.Title,
                Img = advertisement.Img
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AdvertisementEditVM model)
        {
                   
            await _advertisementService.EditAsync(model);

            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
           
            if (id == null)
                return BadRequest(); 

            var advertisement = await _advertisementService.GetByIdAsync(id.Value); 

       
            if (advertisement == null)
                return NotFound();

           
            var detailVM = new AdvertisementDetailVM
            {
                Id = advertisement.Id,
                Img = advertisement.Img,
                Title = advertisement.Title
            };

            return View(detailVM);
        }



    }
}
