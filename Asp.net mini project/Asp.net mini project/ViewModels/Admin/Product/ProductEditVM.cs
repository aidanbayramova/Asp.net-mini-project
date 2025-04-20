using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Product
{
    public class ProductEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Desc { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile>? Images { get; set; }
        public List<ProductImageVM> ExistingImages { get; set; } = new();
        public int? MainImageId { get; set; }
        public string? ExistingImagePath { get; set; }
    }
}
