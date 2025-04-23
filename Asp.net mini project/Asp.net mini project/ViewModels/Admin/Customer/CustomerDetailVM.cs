using System.ComponentModel.DataAnnotations;
using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.Review;

namespace Asp.net_mini_project.ViewModels.Admin.Customer
{
    public class CustomerDetailVM
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string ProfileImg { get; set; }
        [Required]
        public ICollection<ReviewVM> Reviews { get; set; }
    }
}
