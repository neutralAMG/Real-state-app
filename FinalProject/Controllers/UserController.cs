using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Presentation.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IAccountService accountService;

        //public UserController(IAccountService _accountService)
        //{
        //    accountService = _accountService;
        //}

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(string nameormail, string password)
        //{
        //    await accountService.AuthenticateWebAppAsync(nameormail, password);
        //    return View();
        //}
    }
}
