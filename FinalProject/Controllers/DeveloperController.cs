using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Models.User;
using FinalProject.Infraestructure.Identity.Enums;
using FinalProject.Presentation.WebApp.Middleware.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Presentation.WebApp.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public DeveloperController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

		[ServiceFilter(typeof(IsUserNotLogIn))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> MantDeveloper()
        {
            Result<List<UserModel>> result = new();
            try
            {
                result = await _userService.GetAllBySpecificRoleAsync(nameof(Roles.Developer));

                if (result.ISuccess)
                {
                }
                return View(result.Data);
            }
            catch
            {
                throw;
            }
        }

		[ServiceFilter(typeof(IsUserNotLogIn))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateDeveloper()
        {
            return View(new SaveUserModel());
        }

		[ServiceFilter(typeof(IsUserNotLogIn))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDeveloper(SaveUserModel saveModel)
        {
            Result result = new();
            try
            {
                saveModel.PhoneNumber = "1111111111";
                if (saveModel.Password != saveModel.ConfirmPassword)
                {
                    TempData["ErrorMessage"] = "The passwords must match";
                    return View(saveModel);
                }

                result = await _accountService.RegisterAsync(saveModel);

                if (!result.ISuccess)
                {
                    TempData["ErrorMessage"] = result.Message;
                    return View(saveModel);
                }

                TempData["SuccessMessage"] = result.Message;

                return RedirectToAction("MantDeveloper");
            }
            catch
            {
                return View(saveModel);
            }
        }
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditDeveloper(string id)
        {

			if (id == default)
			{
				return NoContent();
			}
			Result<UserModel> result = new();

            try
            {
                result = await _userService.GetByIdAsync(id);

                if (!result.ISuccess)
                {
                    return RedirectToAction("MantDeveloper");
                }

                return View(result.Data);
            }
            catch
            {
                return RedirectToAction("MantDeveloper");
            }
          
        }

		[ServiceFilter(typeof(IsUserNotLogIn))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDeveloper(string id , string OldPassword, string OldConfirmPassword, SaveUserModel saveModel)
        {
			if (id == default || OldPassword == default || OldConfirmPassword == default)
			{
				return NoContent();
			}
			//fix this
			Result result = new();
            try
            {

                if (saveModel.Password is null && saveModel.ConfirmPassword is null)
                {
                    saveModel.Password = OldPassword;
                    saveModel.ConfirmPassword = OldConfirmPassword;
                }
                else if (saveModel.Password != saveModel.ConfirmPassword)
                {
                    TempData["ErrorMessage"] = "The passwords must match";
                    return View("EditUser", id);
                }


                result = await _userService.UpdateUserAsync(saveModel);

                if (!result.ISuccess)
                {
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("EditDeveloper", id);
                }
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("MantDeveloper");

            }
            catch
            {
                return RedirectToAction("MantDeveloper");
            }
        }
    }
}
