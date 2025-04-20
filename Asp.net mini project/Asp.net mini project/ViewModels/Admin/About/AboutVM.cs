using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.About
{
    public class AboutVM
    {
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }
        public IFormFile VideoFile { get; set; }

    }
}
