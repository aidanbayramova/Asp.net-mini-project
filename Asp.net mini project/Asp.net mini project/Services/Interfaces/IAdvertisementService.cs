using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.Advertisement;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface IAdvertisementService
    {
        Task<IEnumerable<Advertisement>> GetAllAsync(); 
        Task<Advertisement> GetByIdAsync(int id); 
        Task CreateAsync(AdvertisementCreateVM request); 
        Task DeleteAsync(Advertisement advertisement);
        Task EditAsync(AdvertisementEditVM advertisement);
    }
}
