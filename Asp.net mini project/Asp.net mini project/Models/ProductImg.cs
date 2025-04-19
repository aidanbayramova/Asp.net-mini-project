namespace Asp.net_mini_project.Models
{
    public class ProductImg:BaseEntity
    {
        public string Img { get; set; }
        public bool IsMain { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
