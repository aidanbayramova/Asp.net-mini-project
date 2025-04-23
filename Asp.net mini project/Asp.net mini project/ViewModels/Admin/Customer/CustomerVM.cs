using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Customer
{
    public class CustomerVM
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string ProfileImg { get; set; }
    }
}
