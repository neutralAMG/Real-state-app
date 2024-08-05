using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Application.Models.User;
using FinalProject.Core.Application.Services.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Chequeando.Controllers
{
	public class UserController : Controller
	{
		private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public UserController(IAccountService accountService, IUserService userService)
		{
			_accountService = accountService;
            _userService = userService;
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
			if (!result.ISuccess)
			{
				ModelState.AddModelError("", result.Message);
				return View();
			}
			return RedirectToAction("About", "Home");
		}

		public IActionResult Register()
		{
			return View(new SaveUserModel());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(SaveUserModel saveModel)
		{
			Result result = new();
			try
			{
				if(saveModel.Password != saveModel.ConfirmPassword)
				{
					ModelState.AddModelError("", "Passwords do not match.");
					return View(saveModel);
				}
				result = await _accountService.RegisterAsync(saveModel);

				if (!result.ISuccess)
				{
					ModelState.AddModelError("", "No se a logrado registrar");
					return View("Redirect", saveModel);
				}
				return RedirectToAction("Login", "User");
			}
			catch
			{
				ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
				return RedirectToAction("Index", "Home");
			}

		}

        public async Task<IActionResult> EditUser(string id)
        {
            Result<UserModel> result = new();
            try
            {
                result = await _userService.GetByIdAsync(id);

                if (!result.ISuccess)
                {

                }

                return View(result.Data);
            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(string id, SaveUserModel saveModel)
        {
            Result result = new();
            try
            {
                result = await _userService.UpdateUserAsync(saveModel);

                if (!result.ISuccess)
                {
                    RedirectToAction("EditProperty", id);
                }

                return RedirectToAction("IndexAgent", "Home");

            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }

        }
    }
}
