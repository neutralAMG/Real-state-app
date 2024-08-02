using FinalProject.Core.Application.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chequeando.Controllers
{
    public class UserController : Controller
    {
        private readonly AccountService _accountService;

        public UserController(AccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginPost(string usernameMail, string password)
        {
            var result = _accountService.AuthenticateWebAppAsync(usernameMail, password);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterPost()
        {
            return RedirectToAction("Login", "User");
        }
    }
}
