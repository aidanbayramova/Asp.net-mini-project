using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.About;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface IAboutService 
    {
        Task<List<AboutVM>> GetAllAsync();
        Task<About> GetByIdAsync(int id);
        Task CreateAsync(AboutCreateVM request);
        Task EditAsync(int id, AboutEditVM request);
        Task DeleteAsync(About about);
        Task<AboutDetailVM> GetDetailAsync(int id);
    }
}
