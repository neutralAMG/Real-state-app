using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Application.Models.PropertyType;
using FinalProject.Presentation.WebApp.Middleware.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Presentation.WebApp.Controllers
{
	public class PropertyTypeController : Controller
	{
		private readonly IPropertyTypeService _propertyTypeService;

		public PropertyTypeController(IPropertyTypeService propertyTypeService)
		{
			_propertyTypeService = propertyTypeService;
		}	
		// GET: PropertyTypeController
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Index()
		{
			Result<List<PropertyTypeModel>> result = new();
			try
			{
				result = await _propertyTypeService.GetAllAsync();

				if (!result.ISuccess)
				{
					return RedirectToAction("IndextAdmin", "Home");
				}
				return View(result.Data);
			}
			catch
			{
				return RedirectToAction("IndexAdmin", "Home");
			}

		}


		// GET: PropertyTypeController/Details/5
		public async Task<IActionResult> Details(int id)
		{
			return View();
		}

		// GET: PropertyTypeController/Create
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreatePropertyType()
		{
			return View(new SavePropertyTypeModel());
		}

		// POST: PropertyTypeController/Create
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreatePropertyType(SavePropertyTypeModel saveModel)
		{
			Result<SavePropertyTypeModel> result = new();
			try
			{
				result = await _propertyTypeService.SaveAsync(saveModel);

				if (!result.ISuccess)
				{
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("CreatePropertyType", saveModel);
				}

                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Index");
			}
			catch
			{
				return RedirectToAction("Index");
			}
		}

		// GET: PropertyTypeController/Edit/5
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditPropertyType(int id)
		{
			if (id == default)
			{
				return NoContent();
			}
			Result<PropertyTypeModel> result = new();
			try
			{
				result = await _propertyTypeService.GetByIdAsync(id);

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

		// POST: PropertyTypeController/Edit/5
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> EditPropertyType(int id, SavePropertyTypeModel saveModel)
		{
			if (id == default)
			{
				return NoContent();
			}
			Result<SavePropertyTypeModel> result = new();
			try
			{
				result = await _propertyTypeService.UpdateAsync(id, saveModel);

				if (!result.ISuccess)
				{
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("Edit", id);
				}
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction(nameof(Index));
			}
			catch
			{
				return RedirectToAction("Index");
			}
		}


		// GET: PropertyTypeController/Delete/5
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeletePropertyType(int id)
		{
			if (id == default)
			{
				return NoContent();
			}
			Result<PropertyTypeModel> result = new();
			try
			{
				result = await _propertyTypeService.GetByIdAsync(id);

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
		// POST: PropertyTypeController/Delete/5
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeletePropertyType(int id, IFormCollection collection)
		{
			if (id == default)
			{
				return NoContent();
			}
			Result result = new();
			try
			{
				result = await _propertyTypeService.DeleteAsync(id);
				if (!result.ISuccess)
				{
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("Delete", id);
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
