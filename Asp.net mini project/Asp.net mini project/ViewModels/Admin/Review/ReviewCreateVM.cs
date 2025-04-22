using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.net_mini_project.ViewModels.Admin.Review
{
    public class ReviewCreateVM
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int CustomerId { get; set; }
        public List<SelectListItem> Customers { get; set; }
    }
}
