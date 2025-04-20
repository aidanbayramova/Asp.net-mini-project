using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.Category;
using FiorelloBackendPB103.Areas.Admin.ViewModels.Category;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryVM>> GetAllAsync();
        Task CreateAsync(CategoryCreateVM request);
        Task DeleteAsync(int id);
        Task<Category> GetByIdAsync(int id);
        Task EditAsync(CategoryEditVM request);
    }
}
