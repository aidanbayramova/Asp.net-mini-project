using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Team;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Services
{
    public class TeamService :ITeamService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamService(
            AppDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
       
        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) throw new Exception("Team member not found");
            return team;
        }
        public async Task CreateAsync(TeamCreateVM model)
        {
            var team = new Team
            {
                FullName = model.FullName,
                Position = model.Position
            };

            if (model.Img != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Img.FileName);
                string uploadPath = Path.Combine(_env.WebRootPath, "img");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                string filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Img.CopyToAsync(stream);
                }

                team.Img = "/img/" + fileName;
            }

            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team == null) throw new Exception("Team member not found");

            if (!string.IsNullOrEmpty(team.Img))
            {
                string imagePath = Path.Combine(_env.WebRootPath, team.Img.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(int id, TeamEditVM model)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(t => t.Id == id);
            if (team == null) throw new Exception("Team not found");

            team.FullName = model.FullName;
            team.Position = model.Position;

            if (model.NewImg != null)
            {
                
                if (!string.IsNullOrWhiteSpace(team.Img))
                {
                    string oldImagePath = Path.Combine(_env.WebRootPath, "img", Path.GetFileName(team.Img));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewImg.FileName);
                string newImagePath = Path.Combine(_env.WebRootPath, "img", fileName);

                using (FileStream stream = new FileStream(newImagePath, FileMode.Create))
                {
                    await model.NewImg.CopyToAsync(stream);
                }

                team.Img = "/img/" + fileName;
            }

            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }
    }
}
