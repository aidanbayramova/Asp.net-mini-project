using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Advertisement
{
    public class AdvertisementCreateVM
    {
        [Required]
        public IFormFile Img { get; set; }
        [Required]
        public string Title { get; set; }
    }
}
