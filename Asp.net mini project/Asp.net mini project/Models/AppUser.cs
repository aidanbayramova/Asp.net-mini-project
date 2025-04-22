using Microsoft.AspNetCore.Identity;

namespace Asp.net_mini_project.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
