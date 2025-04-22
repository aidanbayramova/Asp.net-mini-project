using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asp.net_mini_project.ViewModels.Admin.Review
{
    public class ReviewCreateVM
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int? CustomerId { get; set; }
        public IEnumerable<SelectListItem> Customers { get; set; }
        public int ConsumerId { get; internal set; }
        public List<SelectListItem> Consumers { get; internal set; }
    }
}
