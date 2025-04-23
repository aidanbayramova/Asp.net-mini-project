using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Customer
{
    public class CustomerEditVM
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }
        [Required]
        public string ProfileImg { get; set; }
        [Required]
        public IFormFile? NewProfileImage { get; set; }
    }
}
