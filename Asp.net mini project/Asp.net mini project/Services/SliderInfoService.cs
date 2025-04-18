using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.SliderInfo;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Services
{
    public class SliderInfoService:ISliderInfoService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderInfoService(AppDbContext context,
                             IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        public async Task<IEnumerable<SliderInfo>> GetAllAsync()
        {
            return await _context.SliderInfos.AsNoTracking().ToListAsync();
        }
        public async Task CreateAsync(SliderInfoCreateVM request)
        {
            SliderInfo info = new()
            {
                Title = request.Title,
                Discount = request.Discount,
                Description = request.Description
            };

            await _context.SliderInfos.AddAsync(info);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(SliderInfo sliderInfo)
        {
            _context.SliderInfos.Remove(sliderInfo);
            await _context.SaveChangesAsync();
        }

        public async Task<SliderInfo?> GetByIdAsync(int id)
        {
            return await _context.SliderInfos.FindAsync(id);
        }
        public async Task EditAsync(SliderInfoEditVM model)
        {
            var sliderInfo = await GetByIdAsync(model.Id);
            if (sliderInfo == null) throw new Exception("SliderInfo not found");

            sliderInfo.Title = model.Title;
            sliderInfo.Discount = model.Discount;
            sliderInfo.Description = model.Description;

            _context.SliderInfos.Update(sliderInfo);
            await _context.SaveChangesAsync();
        }

    }
}
