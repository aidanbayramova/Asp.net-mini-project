using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.Models
{
    public class Newsletter
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime CreateDate{ get; set; } = DateTime.Now;
    }
}
