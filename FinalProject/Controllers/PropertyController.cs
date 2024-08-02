using Microsoft.AspNetCore.Mvc;

namespace Chequeando.Controllers
{
    public class PropertyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Detail()
        {
            return View();
        }

        public IActionResult FavoriteProperty()
        {
            return View();
        }
    }
}
