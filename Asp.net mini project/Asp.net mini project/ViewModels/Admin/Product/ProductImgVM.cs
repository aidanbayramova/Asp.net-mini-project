using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Product
{
    public class ProductImageVM
    {
        public int Id { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]
        public bool IsMain { get; set; }
    }
}
