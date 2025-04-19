using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Slider
{
    public class SliderEditVM
    {

        public int Id { get; set; }
        [Required]
        public string? Img { get; set; }
        [Required]
        public IFormFile? Photo { get; set; }
    }
}
