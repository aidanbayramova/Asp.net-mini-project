using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Team
{
    public class TeamEditVM
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Position { get; set; }
        public string Img { get; set; }
        public IFormFile NewImg { get; set; }
    }
}
