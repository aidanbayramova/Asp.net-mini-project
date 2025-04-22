using System.ComponentModel.DataAnnotations;
using Asp.net_mini_project.Models;

namespace Asp.net_mini_project.ViewModels.Admin.Blog
{
    public class BlogVM
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public ICollection<BlogImage> BlogImages { get; set; }

    }
}
