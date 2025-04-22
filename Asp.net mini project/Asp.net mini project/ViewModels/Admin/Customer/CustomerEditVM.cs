using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Customer
{
    public class CustomerEditVM
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        public string ProfileImg { get; set; }

        public IFormFile? NewProfileImage { get; set; }
    }
}
