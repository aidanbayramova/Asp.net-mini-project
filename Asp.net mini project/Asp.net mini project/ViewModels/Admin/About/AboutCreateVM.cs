using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.About
{
    public class AboutCreateVM
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Image is required")]
        public IFormFile Img { get; set; }

        public string VideoUrl { get; set; }
    }
}
