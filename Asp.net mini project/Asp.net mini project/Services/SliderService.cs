using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Slider;
using FiorelloBackendPB103.Helpers.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Asp.net_mini_project.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderService(AppDbContext context,
                             IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(SliderCreateVM request)
        {
            string fileName = Guid.NewGuid().ToString() + "-" + request.Image.FileName;
            string path = Path.Combine(_env.WebRootPath, "img", fileName);

            using (FileStream stream = new(path, FileMode.Create))
            {
                await request.Image.CopyToAsync(stream);
            }

            var slider = new Slider { Img = fileName };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Slider slider)
        {
            string path = Path.Combine(_env.WebRootPath, "img", slider.Img);
            path.Delete();
            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }

     
        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _context.Sliders.FindAsync(id);
        }


        public async Task EditAsync(SliderEditVM model)
        {
            var slider = await GetByIdAsync(model.Id);
            if (slider == null) throw new Exception("Slider not found");

            if (model.Photo != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                string path = Path.Combine(_env.WebRootPath, "img", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }

                if (!string.IsNullOrWhiteSpace(slider.Img))
                {
                    string oldPath = Path.Combine(_env.WebRootPath, "img", slider.Img);
                    if (File.Exists(oldPath))
                    {
                        File.Delete(oldPath);
                    }
                }

                slider.Img = fileName;
            }

            _context.Sliders.Update(slider);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _context.Sliders.AsNoTracking().ToListAsync();
        }

    

    }
}
