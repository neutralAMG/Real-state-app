using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Application.Models.User;
using FinalProject.Core.Application.Services.Identity;
using FinalProject.Infraestructure.Identity.Enums;
using FinalProject.Presentation.WebApp.Middleware.Filters;
using FinalProject.Presentation.WebApp.Middleware.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Chequeando.Controllers
{
	public class UserController : Controller
	{
		private readonly IAccountService _accountService;
		private readonly IUserService _userService;
		private readonly UserSessionInfoValidations _userSessionInfoValidations;

		public UserController(IAccountService accountService, IUserService userService, UserSessionInfoValidations userSessionInfoValidations)
		{
			_accountService = accountService;
			_userService = userService;
			_userSessionInfoValidations = userSessionInfoValidations;
		}

		public IActionResult Index()
		{
			return View();
		}
		[ServiceFilter(typeof(IsUserLogIn))]
		public IActionResult Login()
		{
			return View();
		}
		[ServiceFilter(typeof(IsUserLogIn))]
		public async Task<IActionResult> ConfirmEmail(string userId, string token)
		{
			Result result = new();
			try
			{
				result = await _accountService.ConfirmEmail(userId, token);
			}
			catch
			{

			}
			return View();

		}
		[ServiceFilter(typeof(IsUserLogIn))]
		[HttpPost]
		public async Task<IActionResult> Login(string usernameMail, string password)
		{
			var result = await _accountService.AuthenticateWebAppAsync(usernameMail, password);
			if (!result.ISuccess)
			{
				ModelState.AddModelError("", result.Message);
				return View();
			}

			if (result.Message == nameof(Roles.Client)) return RedirectToAction("IndexLogeado", "Home");
			if (result.Message == nameof(Roles.Agent)) return RedirectToAction("IndexAgent", "Home");
			return RedirectToAction("IndexAdmin", "Home");

		}

		[ServiceFilter(typeof(IsUserLogIn))]
		public IActionResult Register()
		{
			return View(new SaveUserModel());
		}

		[ServiceFilter(typeof(IsUserLogIn))]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(SaveUserModel saveModel)
		{
			Result result = new();
			try
			{
				if (saveModel.Password != saveModel.ConfirmPassword)
				{
					ModelState.AddModelError("", "Passwords do not match.");
					return View(saveModel);
				}
				var origin = Request.Headers["Origin"];
				result = await _accountService.RegisterAsync(saveModel, origin);

				if (!result.ISuccess)
				{
					//ModelState.AddModelError("", "No se a logrado registrar");
					return View(saveModel);
				}
				return RedirectToAction("Login", "User");
			}
			catch
			{
				ModelState.AddModelError("", "An unexpected error occurred. Please try again.");
				return RedirectToAction("Index", "Home");
			}

		}


		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> HabdelUserActiveState(string id)
		{
			if (id == default)
			{
				return NoContent();
			}
			Result result = new();
			try
			{
				result = await _userService.HandleUserActivationStateAsync(id);

				if (!result.ISuccess)
				{

				}
				return Redirect(Request.Headers["Referer"].ToString());
			}
			catch
			{
				return RedirectToAction("IndexAgent", "Home");
			}

		}


		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteUser(string id)
		{
			if (id == default)
			{
				return NoContent();
			}

			Result result = new();
			try
			{
				result = await _userService.DeleteUserAsync(id);

				if (!result.ISuccess)
				{
					RedirectToAction("EditUser", id);
				}

				return Redirect(Request.Headers["Referer"].ToString());

			}
			catch
			{
				return RedirectToAction("MantAdmin", "Home");
			}

		}

	}
}
