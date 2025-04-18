using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.SliderInfo
{
    public class SliderInfoCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Discount { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
