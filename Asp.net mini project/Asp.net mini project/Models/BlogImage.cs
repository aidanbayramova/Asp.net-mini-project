namespace Asp.net_mini_project.Models
{
    public class BlogImage :BaseEntity
    {
        public string Img { get; set; }
        public bool IsMain { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
