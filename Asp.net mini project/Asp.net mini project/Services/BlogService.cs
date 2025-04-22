using Asp.net_mini_project.Data;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.Services.Interfaces;
using Asp.net_mini_project.ViewModels.Admin.Blog;
using Asp.net_mini_project.ViewModels.Admin.ImageBlog;
using Microsoft.EntityFrameworkCore;

namespace Asp.net_mini_project.Services
{
    public class BlogService : IBlogService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnv;

        public BlogService(AppDbContext context, IWebHostEnvironment webHostEnv)
        {
            _context = context;
            _webHostEnv = webHostEnv;
        }

        public async Task CreateAsync(BlogCreateVM createVM)
        {
            var newBlog = new Blog
            {
                Title = createVM.Title,
                Desc = createVM.Description,
                BlogImages = new List<BlogImage>()
            };

            if (createVM.Images != null && createVM.Images.Any())
            {
                foreach (var imageFile in createVM.Images)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                    var filePath = Path.Combine(_webHostEnv.WebRootPath, "img", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    newBlog.BlogImages.Add(new BlogImage
                    {
                        Img = "/img/" + fileName,
                        IsMain = false
                    });
                }

                if (newBlog.BlogImages.Any())
                {
                    newBlog.BlogImages.First().IsMain = true;
                }
            }

            await _context.Blogs.AddAsync(newBlog); 
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Blog blog)
        {
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteImgAsync(int imageId)
        {
            var image = await _context.BlogImages.FindAsync(imageId);
            if (image == null) return;

            var fullPath = Path.Combine("wwwroot", image.Img.TrimStart('/'));
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            _context.BlogImages.Remove(image);
            await _context.SaveChangesAsync();
        }


        public async Task EditAsync(BlogEditVM editVM)
        {
            var existingBlog = await _context.Blogs
                .Include(b => b.BlogImages)
                .FirstOrDefaultAsync(b => b.Id == editVM.Id);

            if (existingBlog == null) throw new Exception("Blog not found");

            existingBlog.Title = editVM.Title;
            existingBlog.Desc = editVM.Description;



            if (editVM.Images != null && editVM.Images.Count > 0)
            {
                foreach (var imageFile in editVM.Images)
                {
                    var uniqueFileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    var savePath = Path.Combine("wwwroot/img", uniqueFileName);

                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    existingBlog.BlogImages.Add(new BlogImage
                    {
                        Img = "/img/" + uniqueFileName,
                        IsMain = false
                    });
                }
            }

            if (editVM.MainImageId.HasValue)
            {
                foreach (var img in existingBlog.BlogImages)
                {
                    img.IsMain = img.Id == editVM.MainImageId.Value;
                }
            }

            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<BlogVM>> GetAllAsync()
        {
            return await _context.Blogs
                .Include(b => b.BlogImages)
                .Select(b => new BlogVM
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Desc,
                    Img = b.BlogImages.FirstOrDefault(img => img.IsMain).Img,
                    CreatedDate = b.CreateDate,
                    BlogImages = b.BlogImages
                })
                .ToListAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _context.Blogs
                .Include(b => b.BlogImages)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BlogDetailVM> GetDetailAsync(int id)
        {
            var blog = await _context.Blogs
                .Include(b => b.BlogImages)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null) return null;

            return new BlogDetailVM
            {
                Title = blog.Title,
                Description = blog.Desc,
                BlogImages = blog.BlogImages,
                CreatedDate = blog.CreateDate,
            };
        }

        public async Task<BlogEditVM> GetEditModelAsync(int id)
        {
            var blog = await _context.Blogs
                .Include(b => b.BlogImages)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null) return null;

            return new BlogEditVM
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Desc,
                ExistingImages = blog.BlogImages.Select(img => new BlogImageVM
                {
                    Id = img.Id,
                    Img = img.Img,
                    IsMain = img.IsMain
                }).ToList(),
                MainImageId = blog.BlogImages.FirstOrDefault(img => img.IsMain)?.Id
            };
        }
    }

}
