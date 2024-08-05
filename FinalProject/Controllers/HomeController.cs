using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Chequeando.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IAccountService _accountService;

		public HomeController(ILogger<HomeController> logger, IAccountService accountService)
		{
			_logger = logger;
			_accountService = accountService;
		}

		public IActionResult Index()
		{
			return View();

		}

		public IActionResult IndexLogeado()
		{
			return View();
		}

		public IActionResult IndexAgent()
		{
			return View();
		}

		public IActionResult IndexAdmin()
		{
			return View();
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
