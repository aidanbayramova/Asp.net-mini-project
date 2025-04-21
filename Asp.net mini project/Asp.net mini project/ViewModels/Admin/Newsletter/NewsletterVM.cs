using System.ComponentModel.DataAnnotations;
using Asp.net_mini_project.Models;

namespace Asp.net_mini_project.ViewModels.Admin.Newsletter
{
    public class NewsletterVM
    {
        public int Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        //public int TotalCount { get; set; }
        public List<NewsletterVM> Newsletters { get; set; }
    }
}
