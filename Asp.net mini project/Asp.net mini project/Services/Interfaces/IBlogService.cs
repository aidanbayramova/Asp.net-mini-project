using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.Blog;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface IBlogService
    {
        Task CreateAsync(BlogCreateVM model);
        Task DeleteImgAsync(int imageId);
        Task EditAsync(BlogEditVM model);
        Task DeleteAsync(Blog blog);
        Task<Blog?> GetByIdAsync(int id);
        Task<IEnumerable<BlogVM>> GetAllAsync();
        Task<BlogDetailVM?> GetDetailAsync(int id);
        Task<BlogEditVM?> GetEditModelAsync(int id);
    }
}
