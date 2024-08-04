using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult MantAdmin()
        {
            return View();
        }

        public IActionResult CreateAdmin()
        {
            return View();
        }

        public IActionResult EditAdmin()
        {
            return View();
        }
    }
}
