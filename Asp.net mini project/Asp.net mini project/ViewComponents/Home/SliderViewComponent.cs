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
        private readonly ISliderService _sliderService;
        private readonly ISliderInfoService _sliderInfoService;

        public SliderViewComponent(ISliderService sliderService,
                                  ISliderInfoService sliderInfoService)
        {
            _sliderService = sliderService;
            _sliderInfoService = sliderInfoService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAllAsync();
            IEnumerable<SliderInfo> sliderInfos = await _sliderInfoService.GetAllAsync();
            return await Task.FromResult(View(new SliderVMVC { Sliders = sliders, SliderInfos = sliderInfos }));
        }
        public class SliderVMVC
        {
          public IEnumerable<Slider> Sliders { get; set; }
            public IEnumerable<SliderInfo> SliderInfos { get; set; }
                
        }
    }
}
