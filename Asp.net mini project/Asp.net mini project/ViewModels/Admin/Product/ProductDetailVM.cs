using System.ComponentModel.DataAnnotations;
using Asp.net_mini_project.Models;

namespace Asp.net_mini_project.ViewModels.Admin.Product
{
    public class ProductDetailVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string Desc { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public ICollection<ProductImg> ProductImgs { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
