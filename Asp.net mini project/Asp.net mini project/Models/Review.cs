namespace Asp.net_mini_project.Models
{
    public class Review : BaseEntity
    {
        public string Comment { get; set; }
        public int ConsumerId { get; set; }
        public Customer Customer { get; set; }
    }
}
