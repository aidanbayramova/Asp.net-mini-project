using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Brand
{
    public class BrandCreateVM
    {
        [Required]
        public IFormFile Img { get; set; }
    }
}
