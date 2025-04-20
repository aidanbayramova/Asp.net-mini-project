using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Category
{
    public class CategoryEditVM
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Category length must be max 30")]
        public string Name { get; set; }
    }
}
