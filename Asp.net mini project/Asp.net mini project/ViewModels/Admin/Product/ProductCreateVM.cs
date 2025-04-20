using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Product
{
    public class ProductCreateVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Desc { get; set; }


        [Required(ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
