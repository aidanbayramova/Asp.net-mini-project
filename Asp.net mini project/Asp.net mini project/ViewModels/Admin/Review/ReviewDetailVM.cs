using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Review
{
    public class ReviewDetailVM
    {
        public int Id { get; set; }
        [Required]
        public string Comment { get; set; }
        [Required]
        public string CustomerFullName { get; set; }
        [Required]
        public string CustomerProfileImg { get; set; }
    }
}
