using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Brand;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Services
{
    public class BrandService :IBrandService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BrandService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<List<BrandVM>> GetAllAsync()
        {
            return await _context.Brands
                .Select(b => new BrandVM
                {
                    Id = b.Id,
                    Img = b.Img
                })
                .ToListAsync();
        }

        public async Task<Brand> GetByIdAsync(int id)
        {
           

            return await _context.Brands.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task CreateAsync(BrandCreateVM request)
        {
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Img.FileName);
            string path = Path.Combine(_env.WebRootPath, "img", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await request.Img.CopyToAsync(stream);
            }

            Brand brand = new Brand
            {
                Img = fileName,
  
            };

            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(BrandEditVM model)
        {
            var brand = await GetByIdAsync(model.Id);
            if (brand == null)
            {
                throw new Exception("Brand not found");
            }

            if (model.Photo != null)
            {
                var directoryPath = Path.Combine(_env.WebRootPath, "img");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, model.Photo.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }

                brand.Img = model.Photo.FileName;
            }

            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return;

            string path = Path.Combine(_env.WebRootPath, "uploads", "brands", brand.Img);
            if (File.Exists(path)) File.Delete(path);

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
        }
    }
}
