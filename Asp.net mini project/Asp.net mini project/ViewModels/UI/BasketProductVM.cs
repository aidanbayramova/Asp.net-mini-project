using System.ComponentModel.DataAnnotations;
using Asp.net_mini_project.Models;

namespace Asp.net_mini_project.ViewModels.UI
{
    public class BasketProductVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public Product Product { get; set; }
    }
}
