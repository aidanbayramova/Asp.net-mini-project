using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.Review;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface IReviewService
    {
        Task<List<ReviewVM>> GetAllAsync();
        Task<ReviewDetailVM> GetByIdAsync(int id);
        Task<ReviewCreateVM> GetCreateModelAsync();
        Task<ReviewEditVM> GetEditModelAsync(int id);
        Task CreateAsync(ReviewCreateVM createVM);
        Task EditAsync(ReviewEditVM editVM);
        Task DeleteAsync(Review review);
    }
}
