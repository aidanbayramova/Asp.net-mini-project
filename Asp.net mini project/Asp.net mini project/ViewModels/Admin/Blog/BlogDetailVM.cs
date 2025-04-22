using Asp.net_mini_project.Models;

namespace Asp.net_mini_project.ViewModels.Admin.Blog
{
    public class BlogDetailVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<BlogImage> BlogImages { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
