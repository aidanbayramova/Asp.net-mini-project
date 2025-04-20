using System.ComponentModel.DataAnnotations;

namespace FiorelloBackendPB103.Areas.Admin.ViewModels.Category
{
    public class CategoryCreateVM
    {

        [Required]
        [MaxLength(30, ErrorMessage = "Category length must be max 30")]
        public string? Name { get; set; }
    }
}