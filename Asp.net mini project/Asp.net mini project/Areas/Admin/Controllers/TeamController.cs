using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> Detail(int id)
        {
            var team = await _teamService.GetByIdAsync(id);

            if (team == null)
                return NotFound();

          
            var model = new TeamDetailVM
            {
                FullName = team.FullName,
                Position = team.Position,
                Img = team.Img
            };

            return View(model);
        }
        public IActionResult Create()
        {
            return View();
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);

            await _teamService.CreateAsync(model);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _teamService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
               
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null) return NotFound();

            var model = new TeamEditVM
            {
                FullName = team.FullName,
                Position = team.Position,
                Img = team.Img
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamEditVM model)
        {
            if (!ModelState.IsValid) return View(model);

            try
            {
                await _teamService.UpdateAsync(id, model);
            }
            catch (Exception ex)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
