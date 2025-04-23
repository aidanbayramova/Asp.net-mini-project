using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Team
{
    public class TeamVM
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public string Img { get; set; }
    }
}
