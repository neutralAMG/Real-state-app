﻿using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Application.Models.User;
using FinalProject.Infraestructure.Identity.Enums;
using FinalProject.Presentation.WebApp.Middleware.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chequeando.Controllers
{
    public class AgentController : Controller
    {
        private readonly IUserService _userService;
        private readonly IPropertyService _propertyService;

        public AgentController(IUserService userService, IPropertyService propertyService)
        {
            _userService = userService;
            _propertyService = propertyService;
        }
        public async Task<IActionResult> Index()
        {
            Result<List<UserModel>> result = new();
            try
            {
                result = await _userService.GetAllBySpecificRoleAsync(nameof(Roles.Agent));
                if (!result.ISuccess)
                {
                    return RedirectToAction("Index", "Home");
                }
                result.Data = result.Data.Where(u => u.IsActive == true).OrderBy(u => u.FirstName).ToList();
                return View(result.Data);
            }
            catch
            {

            }
            return View();
        }

        [ServiceFilter(typeof(IsUserNotLogIn))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AgentList()
        {
            Result<List<UserModel>> result = new();
            try
            {
                result = await _userService.GetAllBySpecificRoleAsync(nameof(Roles.Agent));
                if (!result.ISuccess)
                {
                    return RedirectToAction("IndexAdmin", "Admin");
                }
                return View(result.Data);
            }
            catch
            {
                return RedirectToAction("IndexAdmin", "Admin");
            }
        }


        public async Task<IActionResult> Property(string id)
        {
            if (id == default)
            {
                return NoContent();
            }
            Result<List<PropertyModel>> result = new();
            try
            {
                result = await _propertyService.GetSpecificAgentProperties(id);

                if (!result.ISuccess)
                {
                    return RedirectToAction("Index");
                }
                return View(result.Data);
            }
            catch
            {
                throw;
            }

        }

        [ServiceFilter(typeof(IsUserNotLogIn))]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> Profile()
        {
            Result<UserModel> result = new();
            try
            {
                result = await _userService.GetCurrentUser();

                if (!result.ISuccess)
                {

                    return NoContent();
                }

                return View(result.Data);
            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }

        }

        [ServiceFilter(typeof(IsUserNotLogIn))]
        [Authorize(Roles = "Agent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(string id, SaveUserModel saveModel)
        {

            if (id == default)
            {
                return NoContent();
            }

            Result result = new();
            try
            {
                result = await _userService.UpdateUserAsync(saveModel);

                if (!result.ISuccess)
                {
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("Profile");
                }
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("IndexAgent", "Home");

            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }

        }

        [ServiceFilter(typeof(IsUserNotLogIn))]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> MantProperty()
        {
            Result<List<PropertyModel>> result = new();
            try
            {
                result = await _propertyService.GetAllCurrentAgentUserPropertiesAsync();
                if (!result.ISuccess)
                {
                    return RedirectToAction("IndexAgent", "Home");
                }

                return View(result.Data);
            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }

        }
    }
}
