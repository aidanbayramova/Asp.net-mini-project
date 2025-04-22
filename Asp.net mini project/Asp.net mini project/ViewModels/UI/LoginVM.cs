using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.UI
{
    public class LoginVM
    {
        [Required]
        public string EmailOrUserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
