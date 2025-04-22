using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.About
{
    public class AboutVM
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string? Image { get; set; }
        public string? VideoUrl { get; set; }

    }
}
