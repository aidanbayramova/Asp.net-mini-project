using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.About
{
    public class AboutVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Img { get; set; }

    }
}
