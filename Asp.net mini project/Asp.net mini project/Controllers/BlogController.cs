using Microsoft.AspNetCore.Mvc;

namespace Asp.net_mini_project.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
