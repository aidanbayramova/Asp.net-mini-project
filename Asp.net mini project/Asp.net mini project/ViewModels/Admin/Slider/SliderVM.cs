using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Slider
{
    public class SliderVM
    {
        public int Id { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
