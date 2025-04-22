using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.About
{
    public class AboutEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string VideoUrl { get; set; }
        public string Img { get; set; }
        public IFormFile NewImg { get; set; }
    }
}
