using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Services
{
    public class AboutService :IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AboutService(AppDbContext context,
                             IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IEnumerable<About>> GetAllAsync()
        {
            return await _context.Abouts.AsNoTracking().ToListAsync();

        }
    }
}
