using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.net_mini_project.ViewModels.Admin.Review
{
    public class ReviewEditVM
    {
        public int Id { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public IEnumerable<SelectListItem> Customers { get; set; }
    }
}
