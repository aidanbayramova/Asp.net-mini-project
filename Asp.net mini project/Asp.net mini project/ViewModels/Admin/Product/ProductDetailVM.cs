using Asp.net_mini_project.Models;

namespace Asp.net_mini_project.ViewModels.Admin.Product
{
    public class ProductDetailVM
    {
        public string Name { get; set; }
        public string CategoryName { get; set; }
        public string Desc { get; set; }
        public decimal Price { get; set; }
        public ICollection<ProductImg> ProductImgs { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
