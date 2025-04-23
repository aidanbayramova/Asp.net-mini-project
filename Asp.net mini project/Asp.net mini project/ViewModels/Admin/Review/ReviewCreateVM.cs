using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.net_mini_project.ViewModels.Admin.Review
{
    public class ReviewCreateVM
    {
        public int Id { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public List<SelectListItem> Customers { get; set; }
    }
}
