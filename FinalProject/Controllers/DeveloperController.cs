using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Presentation.WebApp.Controllers
{
    public class DeveloperController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MantDeveloper()
        {
            return View();
        }
        public IActionResult CreateDeveloper()
        {
            return View();
        }
        public IActionResult EditDeveloper()
        {
            return View();
        }
    }
}
