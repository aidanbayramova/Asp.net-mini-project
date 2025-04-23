using Asp.net_mini_project.Models;

namespace Asp.net_mini_project.ViewModels.UI
{
    public class BasketProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Img { get; set; }
        public int Count { get; set; }
        public string CategoryName { get; set; }
        public Product Product { get; set; }
    }
}
