using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
