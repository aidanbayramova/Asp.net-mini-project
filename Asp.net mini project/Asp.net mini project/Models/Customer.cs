using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.Models
{
    public class Customer : BaseEntity
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; }
        public string ProfileImg { get; set; }
        public ICollection<Review> Reviews { get; set; }
    }
}
