using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.Brand;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface IBrandService
    {
        Task<List<BrandVM>> GetAllAsync();
        Task<Brand> GetByIdAsync(int id);
        Task CreateAsync(BrandCreateVM request);
        Task EditAsync(BrandEditVM model);
        Task DeleteAsync(int id);
    }
}
