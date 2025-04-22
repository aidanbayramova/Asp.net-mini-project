using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.About
{
    public class AboutEditVM
    {
        public int Id { get; set; } 

        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required, MaxLength(1000)]
        public string Description { get; set; }

        public IFormFile? ImageFile { get; set; } 

        public string? CurrentImage { get; set; } 

        [Required, Url]
        public string VideoUrl { get; set; }
    }
}
