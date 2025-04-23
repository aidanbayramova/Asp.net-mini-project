using System.IO;
using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Product;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductService(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IEnumerable<ProductVM>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.ProductImgs)
                .Select(p => new ProductVM
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    Description = p.Desc,
                    Img = p.ProductImgs.FirstOrDefault(pi => pi.IsMain).Img,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name
                })
                .ToListAsync();
        }

        public async Task<ProductDetailVM?> GetDetailAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductImgs)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return null;

            return new ProductDetailVM
            {
                Name = product.Name,
                Price = product.Price,
                CategoryName = product.Category.Name,
                ProductImgs = product.ProductImgs,
                CreateDate = DateTime.Now,
                Desc = product.Desc
            };
        }

        public async Task<ProductEditVM?> GetEditModelAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductImgs)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return null;

            return new ProductEditVM
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Desc = product.Desc,
                CategoryId = product.CategoryId,
                ExistingImages = product.ProductImgs.Select(pi => new ProductImageVM
                {
                    Id = pi.Id,
                    Img = pi.Img,
                    IsMain = pi.IsMain
                }).ToList(),
                MainImageId = product.ProductImgs.FirstOrDefault(pi => pi.IsMain)?.Id
            };
        }

        public async Task CreateAsync(ProductCreateVM model)
        {

          
            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                Desc = model.Desc,
                CategoryId = model.CategoryId,
                ProductImgs = new List<ProductImg>()
            };

            foreach (var image in model.Images)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);


                var path = Path.Combine(_env.WebRootPath, "img", fileName);


                var directoryPath = Path.Combine(_env.WebRootPath, "img");
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                product.ProductImgs.Add(new ProductImg
                {
                    Img = "/img/" + fileName,  
                    IsMain = false
                });
            }

            if (product.ProductImgs.Any())
            {
                product.ProductImgs.First().IsMain = true;
            }

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ProductEditVM model)
        {
            var product = await _context.Products
                .Include(p => p.ProductImgs)
                .FirstOrDefaultAsync(p => p.Id == model.Id);

            if (product == null) return;

            
            product.Name = model.Name;
            product.Price = model.Price;
            product.Desc = model.Desc;
            product.CategoryId = model.CategoryId;

          
            if (model.Images != null && model.Images.Count > 0)
            {
                foreach (var image in model.Images)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                    var path = Path.Combine(_env.WebRootPath, "img", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    product.ProductImgs.Add(new ProductImg
                    {
                        Img = "/img/" + fileName,
                        IsMain = false
                    });
                }
            }

            if (model.MainImageId.HasValue)
            {
                foreach (var image in product.ProductImgs)
                {
                    image.IsMain = image.Id == model.MainImageId.Value;
                }
            }

          
            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                 .Include(m => m.Category)
                .Include(p => p.ProductImgs)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task DeleteImageAsync(int imageId)
        {
            var image = await _context.ProductImgs.FindAsync(imageId);
            if (image == null) return;

           
            var fullPath = Path.Combine("wwwroot", image.Img.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            _context.ProductImgs.Remove(image);
            await _context.SaveChangesAsync();
        }
    
    }
}
