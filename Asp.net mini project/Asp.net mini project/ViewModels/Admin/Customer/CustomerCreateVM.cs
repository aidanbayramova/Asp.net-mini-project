using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Customer
{
    public class CustomerCreateVM
    {
       
        [Required(ErrorMessage = "Full name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full name can only contain letters and spaces.")]
        public string FullName { get; set; }

        [Required]
        public IFormFile ProfileImageFile { get; set; }
    }
}
