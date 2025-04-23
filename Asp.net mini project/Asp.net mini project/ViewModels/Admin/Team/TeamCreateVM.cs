using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Team
{
    public class TeamCreateVM
    {
        [Required(ErrorMessage = "Full name is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full name can only contain letters and spaces.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Position is required.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Position can only contain letters and spaces.")]
        public string Position { get; set; }
        [Required]
        public IFormFile Img { get; set; }
    }
}
