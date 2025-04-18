using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.SliderInfo;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface ISliderInfoService
    {
        Task<IEnumerable<SliderInfo>> GetAllAsync();
        Task CreateAsync(SliderInfoCreateVM request);
        Task<SliderInfo> GetByIdAsync(int id);
        Task DeleteAsync(SliderInfo sliderInfo);
        Task EditAsync(SliderInfoEditVM model);
    }
}
