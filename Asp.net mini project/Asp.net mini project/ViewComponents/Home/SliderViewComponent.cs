using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Slider;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.ViewComponents.Home
{
    public class SliderViewComponent:ViewComponent
    {
        private readonly SliderService _sliderService;
        private readonly ISliderInfoService _sliderInfoService;

        public SliderViewComponent(SliderService sliderService,
                                  ISliderInfoService sliderInfoService)
        {
            _sliderService = sliderService;
            _sliderInfoService = sliderInfoService;
        }
        //public async Task<IViewComponentResult> InvokeAsync()
        //{
        //    IEnumerable<Slider> sliders = await _context.Sliders.AsNoTracking().ToListAsync();
        //    SliderInfo sliderInfo = await _context.SliderInfos.AsNoTracking().FirstOrDefaultAsync();
        //    return await Task.FromResult(View(new SliderVMVC { Sliders = sliders, SliderInfo = sliderInfo }));
        //}
    }
}
