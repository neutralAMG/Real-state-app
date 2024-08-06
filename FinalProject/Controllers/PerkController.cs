using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Models.Perk;
using FinalProject.Presentation.WebApp.Middleware.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Presentation.WebApp.Controllers
{
	public class PerkController : Controller
	{
		private readonly IPerkService _perkService;

		public PerkController(IPerkService perkService)
		{
			_perkService = perkService;
		}
		// GET: PerkController
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Index()
		{
			Result<List<PerkModel>> result = new();
			try
			{
				result = await _perkService.GetAllAsync();

				if (!result.ISuccess)
				{
					return RedirectToAction("IndexAdmin", "Home");
				}

				return View(result.Data);
			}
			catch
			{

			}
			return View();
		}



		// GET: PerkController/Details/5
		public async Task<IActionResult> Details(int id)
		{
			return View();
		}

		// GET: PerkController/Create
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreatePerk()
		{

			return View(new SavePerkModel());
		}

		// POST: PerkController/Create
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreatePerk(SavePerkModel saveModel)
		{
			Result<SavePerkModel> result = new();
			try
			{
				result = await _perkService.SaveAsync(saveModel);

				if (!result.ISuccess)
				{
                    TempData["ErrorMessage"] = result.Message;
                    return View(saveModel);
				}
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: PerkController/Edit/5
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditPerk(int id)
		{
			if (id == default)
			{
				return NoContent();
			}

			Result<PerkModel> result = new();
			try
			{
				result = await _perkService.GetByIdAsync(id);

				if (!result.ISuccess)
				{
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("Index");
				}

				return View(result.Data);
			}
			catch
			{
				return RedirectToAction("IndexAdmin", "Home");
			}

		}

		// POST: PerkController/Edit/5
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditPerk(int id, SavePerkModel saveModel)
		{
			if (id == default)
			{
				return NoContent();
			}

			Result result = new();
			try
			{
				result = await _perkService.UpdateAsync(id, saveModel);

				if (!result.ISuccess)
				{
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("EditPerk", id);
				}
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
			}
			catch
			{
				return RedirectToAction("IndexAdmin", "Home");
			}
		}
		// GET: PerkController/Delete/5
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeletePerk(int id)
		{
			if (id == default)
			{
				return NoContent();
			}

			Result<PerkModel> result = new();
			try
			{
				result = await _perkService.GetByIdAsync(id);

				if (!result.ISuccess)
				{
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("Index");
				}

				return View(result.Data);
			}
			catch
			{

			}
			return View();
		}
		// POST: PerkController/Delete/5
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePerk(int id, IFormCollection collection)
		{
			if (id == default)
			{
				return NoContent();
			}

			Result result = new();
			try
			{
				result = await _perkService.DeleteAsync(id);

				if (!result.ISuccess)
				{
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("Index", id);
				}
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index");
			}
			catch
			{
				return RedirectToAction("Index");
			}
		}
	}
}
