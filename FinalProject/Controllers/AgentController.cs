using Microsoft.AspNetCore.Mvc;

namespace Chequeando.Controllers
{
    public class AgentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Property()
        {
            return View();
        }
    }
}
