namespace Asp.net_mini_project.ViewModels.Admin.Category
{
    public class CategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; internal set; }
    }
}
