using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Models.SellType;
using FinalProject.Presentation.WebApp.Middleware.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Presentation.WebApp.Controllers
{
    public class SellTypeController : Controller
    {
        private readonly ISellTypeService _sellTypeService;

        public SellTypeController(ISellTypeService sellTypeService)
        {
            _sellTypeService = sellTypeService;
        }
		// GET: SellTypeController
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Index()
        {
            Result<List<SellTypeModel>> result = new();
            try
            {
                result = await _sellTypeService.GetAllAsync();

                if (!result.ISuccess)
                {
                    return RedirectToAction("IndexAdmin", "Home");
                }
                return View(result.Data);
            }
            catch
            {
                return RedirectToAction("IndexAdmin", "Home");
            }
          
        }




		// GET: SellTypeController/Details/5
		public async Task<IActionResult> Details(int id)
        {
            return View();
        }

		
		// GET: SellTypeController/Create
        [ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateSellType()
        {
            return View(new SaveSellTypeModel());
        }

	// POST: SellTypeController/Create
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSellType(SaveSellTypeModel saveModel)
        {
            Result<SaveSellTypeModel> result = new();
            try
            {
                result = await _sellTypeService.SaveAsync(saveModel);

                if (!result.ISuccess)
                {
                    return View(saveModel);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

		// GET: SellTypeController/Edit/5
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> EditSellType(int id)
        {
			if (id == default)
			{
				return NoContent();
			}
			Result<SellTypeModel> result = new();
            try
            {
                result = await _sellTypeService.GetByIdAsync(id);

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

		// POST: SellTypeController/Edit/5
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSellType(int id, SaveSellTypeModel saveModel)
        {
			if (id == default)
			{
				return NoContent();
			}
			Result result = new();
            try
            {
                result = await _sellTypeService.UpdateAsync(id, saveModel);

                if (!result.ISuccess)
                {
                    return RedirectToAction("EditSellType", saveModel);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

		// GET: SellTypeController/Delete/5
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteSellType(int id)
        {
			if (id == default)
			{
				return NoContent();
			}
			Result<SellTypeModel> result = new();
            try
            {
                result = await _sellTypeService.GetByIdAsync(id);
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

		// POST: SellTypeController/Delete/5
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[ServiceFilter(typeof(IsTheUserActive))]
		[Authorize(Roles = "Admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSellType(int id, IFormCollection collection)
        {
			if (id == default)
			{
				return NoContent();
			}

			Result result = new();
            try
            {
                result = await _sellTypeService.DeleteAsync(id);

                if (!result.ISuccess)
                {
                    return RedirectToAction("DeleteSellType", id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
