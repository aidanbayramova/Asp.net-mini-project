using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.Models
{
    public class About : BaseEntity
    {
        [Required, MaxLength(100)]
        public string Title { get; set; }    
        [Required]
        public string Description { get; set; }  
        public string Video { get; set; }  
    }
}
