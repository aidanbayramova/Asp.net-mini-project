using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Category;
using FiorelloBackendPB103.Areas.Admin.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<CategoryVM>> GetAllAsync()
        {
            var categries = await _context.Categories.Include(x => x.Products)
                .AsNoTracking()
                .Select(m => new CategoryVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    ProductCount = m.Products.Count()
                }).ToListAsync();


            return categries;

        }
        public async Task CreateAsync(CategoryCreateVM request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new Exception("Category name cannot be empty.");
            }

            bool existCategory = await _context.Categories
                .AnyAsync(c => c.Name.Trim().ToLower() == request.Name.Trim().ToLower());

            if (existCategory)
            {
                throw new Exception("Category already exists.");
            }

            Category category = new Category
            {
                Name = request.Name
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
           
            var category = await _context.Categories.Include(c => c.Products)
                                                     .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                throw new Exception("Category not found.");
            }

           
            _context.Products.RemoveRange(category.Products);

      
            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();
        }
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Products) 
                .FirstOrDefaultAsync(c => c.Id == id); 
        }
        public async Task EditAsync(CategoryEditVM request)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(m => m.Id == request.Id);
            if (category == null) throw new Exception("Category not found.");

            bool existCategory = await _context.Categories
                .AnyAsync(m => m.Name.Trim().ToLower() == request.Name.Trim().ToLower() && m.Id != request.Id);

            if (existCategory) throw new Exception("Category already exists.");

            category.Name = request.Name;
            await _context.SaveChangesAsync();
        }
    }
}

