using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Models.User;
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
					return View(saveModel);
				}
				result = await _accountService.RegisterAsync(saveModel);

				if (!result.ISuccess)
				{
					return View("Redirect", saveModel);
				}
				return RedirectToAction("Login", "User");
			}
			catch
			{
				return RedirectToAction("Index", "Home");
			}

		}
	}
}
