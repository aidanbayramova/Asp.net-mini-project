using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.About;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Services
{
    public class AboutService : IAboutService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public AboutService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<AboutVM>> GetAllAsync()
        {
            return await _context.Abouts
                .Select(a => new AboutVM
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Img = a.Image
                })
                .ToListAsync();
        }

        public async Task<About> GetByIdAsync(int id)
        {
            var about = await _context.Abouts.FirstOrDefaultAsync(a => a.Id == id);
            if (about == null) return null;

            return about;
        }

        public async Task CreateAsync(AboutCreateVM model)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Img.FileName);
            string filePath = Path.Combine(_env.WebRootPath, "img", fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.Img.CopyToAsync(stream);
            }

            var about = new About
            {
                Title = model.Title,
                Description = model.Description,
                VideoUrl = model.VideoUrl,
                Image = "/img/" + fileName
            };

            await _context.Abouts.AddAsync(about);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, AboutEditVM model)
        {
            var about = await _context.Abouts.FirstOrDefaultAsync(a => a.Id == id);
            if (about == null) throw new Exception("About not found");

            about.Title = model.Title;
            about.Description = model.Description;
            about.VideoUrl = model.VideoUrl;

            if (model.NewImg != null)
            {
             
                var oldPath = Path.Combine(_env.WebRootPath, about.Image.TrimStart('/'));
                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }

               
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.NewImg.FileName);
                var newPath = Path.Combine(_env.WebRootPath, "img", fileName);

                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    await model.NewImg.CopyToAsync(stream);
                }

                about.Image = "/img/" + fileName;
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(About about)
        {
            if (about == null) throw new ArgumentNullException(nameof(about));

            _context.Abouts.Remove(about);

            await _context.SaveChangesAsync();
        }
        public async Task<AboutDetailVM> GetDetailAsync(int id)
        {
            var about = await _context.Abouts.FirstOrDefaultAsync(a => a.Id == id);
            if (about == null) return null;

            return new AboutDetailVM
            {
                Id = about.Id,
                Title = about.Title,
                Description = about.Description,
                Img = about.Image
            };
        }
    }
}
