using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.Product;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductVM>> GetAllAsync();
        Task CreateAsync(ProductCreateVM model);
        Task<ProductDetailVM> GetDetailAsync(int id);
        Task<ProductEditVM> GetEditModelAsync(int id); 
        Task EditAsync(ProductEditVM model);
        Task DeleteAsync(Product product);
        Task<Product> GetByIdAsync(int id);
        Task DeleteImageAsync(int imageId);
    }
}
