﻿using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Models;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Models;
using FinalProject.Presentation.WebApp.Middleware.Filters;
using FinalProject.Presentation.WebApp.Utils.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Chequeando.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountService _accountService;
        private readonly IPropertyService _propertyService;
        private readonly IHomeStatisticsService _homeStatisticsService;
        private readonly ISelectListGenerator _selectListGenerator;

        public HomeController(ILogger<HomeController> logger, IAccountService accountService, IPropertyService propertyService, IHomeStatisticsService homeStatisticsService, ISelectListGenerator selectListGenerator)
        {
            _logger = logger;
            _accountService = accountService;
            _propertyService = propertyService;
            _homeStatisticsService = homeStatisticsService;
            _selectListGenerator = selectListGenerator;
        }

        public async Task<IActionResult> Index()
        {
            Result<List<PropertyModel>> result = new();
            try
            {
                result = await _propertyService.GetAllAsync();
                ViewBag.PropertyTypes = await _selectListGenerator.GeneratePropertyTypesSelectListAsync();
                return View(result.Data);
            }
            catch
            {

            }
            return View();

        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Filter(string view, PropertyFilterModel propertyFilterModel)
		{
			if (view == default || propertyFilterModel == null)
			{
				return NoContent();
			}
			Result<List<PropertyModel>> result = new();
			try
			{
				result = await _propertyService.FilterProperties(propertyFilterModel);

				if (!result.ISuccess)
				{
					return NoContent();
				}

				return View(view, result.Data);

			}
			catch
			{
				return RedirectToAction("IndexAgent", "Home");
			}

		}

		[ServiceFilter(typeof(IsUserNotLogIn))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> IndexLogeado()
        {
            Result<List<PropertyModel>> result = new();
            try
            {
                result = await _propertyService.GetAllAsync();
				ViewBag.PropertyTypes = await _selectListGenerator.GeneratePropertyTypesSelectListAsync();
				return View(result.Data);
            }
            catch
            {
                throw;
            }

        }
        [ServiceFilter(typeof(IsUserNotLogIn))]
        public async Task<IActionResult> NoAuthorize()
        {
            return View();
        }

        [ServiceFilter(typeof(IsUserNotLogIn))]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> IndexAgent()
        {
            Result<List<PropertyModel>> result = new();
            try
            {
                result = await _propertyService.GetAllCurrentAgentUserPropertiesAsync();

                if (!result.ISuccess)
                {
                    return RedirectToAction("Index");
                }
				ViewBag.PropertyTypes = await _selectListGenerator.GeneratePropertyTypesSelectListAsync();
				return View(result.Data);
            }
            catch
            {
                return RedirectToAction("Index");
            }

        }
        [ServiceFilter(typeof(IsUserNotLogIn))]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IndexAdmin()
        {
            Result<HomeViewStatisticsModel> result = new();
            try
            {
                result = await _homeStatisticsService.GetAdminStatisticsAsync();

                return View(result.Data);
				ViewBag.PropertyTypes = await _selectListGenerator.GeneratePropertyTypesSelectListAsync();
			}
            catch
            {
                throw;
            }

        }
        [ServiceFilter(typeof(IsUserNotLogIn))]
        public async Task<IActionResult> LogOut()
        {
            Result result = new();
            try
            {
                result = await _accountService.SignOutAsync();

                if (!result.ISuccess)
                {
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                throw;
            }

        }

        [ServiceFilter(typeof(IsUserNotLogIn))]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> MyProperties()
        {
            Result<List<PropertyModel>> result = new();
            try
            {
                result = await _propertyService.GetAllCurrentClientUserFavPropertiesAsync();
				ViewBag.PropertyTypes = await _selectListGenerator.GeneratePropertyTypesSelectListAsync();

				return View(result.Data);
            }
            catch
            {
                throw;
            }

        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
