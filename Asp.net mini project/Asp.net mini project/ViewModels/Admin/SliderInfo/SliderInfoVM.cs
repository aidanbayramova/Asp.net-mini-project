using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.SliderInfo
{
    public class SliderInfoVM
    {
        public int Id { get; set; }
        [Required]
        public string Discount { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
