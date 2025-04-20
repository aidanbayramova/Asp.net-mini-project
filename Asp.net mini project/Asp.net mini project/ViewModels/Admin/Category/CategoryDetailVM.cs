using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Category
{
    public class CategoryDetailVM
    {
        [Required]
        public string Name { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int ProductCount { get; internal set; }
    }
}
