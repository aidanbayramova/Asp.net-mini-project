using System.ComponentModel.DataAnnotations;

namespace Asp.net_mini_project.ViewModels.Admin.Blog
{
    public class BlogCreateVM
    {
        [Required(ErrorMessage = "Başlıq boş qoyula bilməz")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Açıqlama boş qoyula bilməz")]
        public string Description { get; set; }
        public List<IFormFile> Images { get; set; }


    }
}
