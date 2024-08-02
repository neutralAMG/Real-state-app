
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Presentation.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IAccountService accountService;





        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(string nameormail, string password)
        //{
        //    await accountService.AuthenticateWebAppAsync(nameormail, password);
        //    return View();
        //}

    }
}
