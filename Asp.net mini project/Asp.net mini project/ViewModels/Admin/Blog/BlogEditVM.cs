using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.ImageBlog;

namespace Asp.net_mini_project.ViewModels.Admin.Blog
{
    public class BlogEditVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<BlogImageVM> ExistingImages { get; set; } = new();
        public int? MainImageId { get; set; }
        public string? ExistingImagePath { get; set; }
    }
}
