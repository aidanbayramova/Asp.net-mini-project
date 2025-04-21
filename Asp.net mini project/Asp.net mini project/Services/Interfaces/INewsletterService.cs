using Asp.net_mini_project.Models;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface INewsletterService
    {
        Task AddAsync(string email);
        Task<List<Newsletter>> GetAllAsync();
        Task DeleteAsync(int id);
        Task<bool> CheckEmailExistsAsync(string email);
    }
}
