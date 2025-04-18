using Asp.net_mini_project.Models;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderInfo> SliderInfos { get; set; }
    }
}
