namespace Asp.net_mini_project.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Desc { get; set; }
        public ICollection<BlogImage> BlogImages { get; set; }
    }
}
