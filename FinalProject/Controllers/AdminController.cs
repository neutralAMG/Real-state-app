﻿using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Models.User;
using FinalProject.Infraestructure.Identity.Enums;
using FinalProject.Presentation.WebApp.Middleware.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public AdminController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }

        [ServiceFilter(typeof(IsUserNotLogIn))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> MantAdmin()
        {
            Result<List<UserModel>> result = new();
            try
            {
                result = await _userService.GetAllBySpecificRoleAsync(nameof(Roles.Admin));

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
        public async Task<IActionResult> CreateAdmin()
        {
            return View(new SaveUserModel());
        }

        [ServiceFilter(typeof(IsUserNotLogIn))]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdmin(SaveUserModel saveModel)
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

                return RedirectToAction("MantAdmin", "Admin");
            }
            catch
            {
                return View(saveModel);
            }

        }

        [ServiceFilter(typeof(IsUserNotLogIn))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditAdmin(string id)
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
                    return RedirectToAction("MantAdmin", "Admin");
                }

                return View(result.Data);
            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }

        }

        [ServiceFilter(typeof(IsUserNotLogIn))]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAdmin(string id, string OldPassword, string OldConfirmPassword, SaveUserModel saveModel)
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
                    return View("EditAdmin", id);
                }


                result = await _userService.UpdateUserAsync(saveModel);

                if (!result.ISuccess)
                {
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("EditAdmin", id);
                }
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("MantAdmin");

            }
            catch
            {
                return RedirectToAction("MantAdmin");
            }

        }
    }
}
