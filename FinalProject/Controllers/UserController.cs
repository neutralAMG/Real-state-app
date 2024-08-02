
﻿using Microsoft.AspNetCore.Mvc;


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace Chequeando.Controllers
{
    public class UserController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginPost()
        {
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
