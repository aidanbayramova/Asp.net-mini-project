using Asp.net_mini_project.Models;
using Asp.net_mini_project.ViewModels.Admin.Review;

namespace Asp.net_mini_project.ViewModels.Admin.Customer
{
    public class CustomerDetailVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ProfileImg { get; set; }
        public ICollection<ReviewVM> Reviews { get; set; }
    }
}
