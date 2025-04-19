using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.Models
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Desc { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImg> ProductImgs { get; set; }
    }
}
