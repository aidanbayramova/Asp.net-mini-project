using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Customer
{
    public class CustomerCreateVM
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        public IFormFile ProfileImageFile { get; set; }
    }
}
