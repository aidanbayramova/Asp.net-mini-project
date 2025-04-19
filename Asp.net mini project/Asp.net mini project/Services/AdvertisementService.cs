using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Advertisement;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Services
{
    public class AdvertisementService :IAdvertisementService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AdvertisementService(AppDbContext context,
                             IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IEnumerable<Advertisement>> GetAllAsync()
        {
            return await _context.Advertisements.AsNoTracking().ToListAsync();

        }

        public async Task CreateAsync(AdvertisementCreateVM request)
        {
          
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Img.FileName);
            string path = Path.Combine(_env.WebRootPath, "img", fileName);

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await request.Img.CopyToAsync(stream);
            }

            Advertisement advertisement = new Advertisement
            {
                Img = fileName,
                Title = request.Title
            };

            await _context.Advertisements.AddAsync(advertisement);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Advertisement advertisement)
        {
            _context.Advertisements.Remove(advertisement);
            await _context.SaveChangesAsync();
        }

        public async Task<Advertisement> GetByIdAsync(int id)
        {
            return await _context.Advertisements.FirstOrDefaultAsync(a => a.Id == id); 
        }
        public async Task EditAsync(AdvertisementEditVM advertisement)
        {
            var advertisementEntity = await GetByIdAsync(advertisement.Id);
            if (advertisementEntity == null)
            {
                throw new Exception("Advertisement not found");
            }

            advertisementEntity.Title = advertisement.Title;

            // Check if a new photo is uploaded
            if (advertisement.Photo != null)
            {
                // Define the path to save the file
                var directoryPath = Path.Combine(_env.WebRootPath, "img");

               
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var filePath = Path.Combine(directoryPath, advertisement.Photo.FileName);

                // Save the uploaded file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await advertisement.Photo.CopyToAsync(stream);
                }

                // Update the advertisement entity with the new image path
                advertisementEntity.Img = advertisement.Photo.FileName;
            }

            _context.Advertisements.Update(advertisementEntity);
            await _context.SaveChangesAsync();
        }




    }
}
