using Asp.net_mini_project.Models;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface IAboutService 
    {
        Task<IEnumerable<About>> GetAllAsync();
    }
}
