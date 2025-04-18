using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.Slider;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface ISliderService 
    {
        Task<IEnumerable<Slider>> GetAllAsync();
        Task CreateAsync(SliderCreateVM slider);
        Task DeleteAsync(Slider slider);
        Task EditAsync(SliderEditVM model);
        Task<Slider> GetByIdAsync(int id);
    }
}
