using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Services.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chequeando.Controllers
{
    public class UserController : Controller
    {
        private readonly IAccountService _accountService;

        public UserController(IAccountService accountService)
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
        public async Task<IActionResult> Login(string usernameMail, string password)
        {
            var result = await _accountService.AuthenticateWebAppAsync(usernameMail, password);
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
