using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Team;
using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.ViewComponents.About
{
    public class TeamViewComponent : ViewComponent
    {
        private readonly ITeamService _teamService;

        public TeamViewComponent(ITeamService teamService)
        {
            _teamService = teamService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var teams = await _teamService.GetAllAsync();
            var teamVMs = teams.Select(t => new TeamVM
            {
                Id = t.Id,
                FullName = t.FullName,
                Position = t.Position,
                Img = t.Img
            }).ToList();

            return View(teamVMs);
        }
    }
}
