using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.UI
{
    public class BasketVM
    {
        public int ProductId { get; set; }
        [Required]
        public int ProductCount { get; set; }
    }
}
