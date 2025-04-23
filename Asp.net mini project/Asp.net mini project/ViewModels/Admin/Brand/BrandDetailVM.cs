using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Brand
{
    public class BrandDetailVM
    {
        public int Id { get; set; }
        [Required]
        public string Img { get; set; }
    }
}
