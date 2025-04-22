using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.Models
{
    public class About : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }
        [Required, MaxLength(1000)]
        public string Description { get; set; }
        [MaxLength(255)]
        public string? Image { get; set; }
        [MaxLength(500)]
        public string? VideoUrl { get; set; }
    }
}
