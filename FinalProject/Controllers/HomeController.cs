using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Models;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Models;
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

		public HomeController(ILogger<HomeController> logger, IAccountService accountService, IPropertyService propertyService, IHomeStatisticsService homeStatisticsService)
		{
			_logger = logger;
			_accountService = accountService;
            _propertyService = propertyService;
			_homeStatisticsService = homeStatisticsService;
		}

		public async Task<IActionResult> Index()
		{
			Result<List<PropertyModel>> result = new();
			try
			{
				result = await _propertyService.GetAllAsync();

				return View(result.Data);
			}
			catch
			{

			}
			return View();

		}

		public async Task<IActionResult> IndexLogeado()
		{
			return View();
		}

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

				return View(result.Data);
			}
			catch
			{
				return RedirectToAction("Index");
			}
		
		}

		public async Task<IActionResult> IndexAdmin()
		{
			Result<HomeViewStatisticsModel> result = new();
			try
			{
				result = await _homeStatisticsService.GetAdminStatisticsAsync();

				return View(result.Data);
			}
			catch
			{
				throw;
			}
			
		}

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
