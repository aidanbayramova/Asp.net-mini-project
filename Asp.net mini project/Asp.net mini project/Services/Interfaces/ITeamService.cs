using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.Team;

namespace Asp.net_mini_project.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<Team>> GetAllAsync();
        Task<Team> GetByIdAsync(int id);
        Task CreateAsync(TeamCreateVM model);
        Task DeleteAsync(int id);
        Task UpdateAsync(int id, TeamEditVM model);

    }
}
