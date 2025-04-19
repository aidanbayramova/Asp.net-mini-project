using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Advertisement
{
    public class AdvertisementEditVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Img { get; set; }
        [Required]
        public IFormFile? Photo { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
