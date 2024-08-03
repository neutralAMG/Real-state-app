using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Models.Perk;
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
        public async Task<IActionResult> CreatePerk()
        {

            return View(new SavePerkModel());
        }

        // POST: PerkController/Create
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
                    return View(saveModel);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PerkController/Edit/5
        public async Task<IActionResult> EditPerk(int id)
        {
            Result<PerkModel> result = new();
            try
            {
                result = await _perkService.GetByIdAsync(id);

                if (!result.ISuccess)
                {
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPerk(int id, SavePerkModel saveModel)
        {
            Result result = new();
            try
            {
                result = await _perkService.UpdateAsync(id, saveModel);

                if (!result.ISuccess)
                {
                    return RedirectToAction("EditPerk", id);
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction("IndexAdmin", "Home");
            }
        }

        // GET: PerkController/Delete/5
        public async Task<IActionResult> DeletePerk(int id)
        {
            Result<PerkModel> result = new();
            try
            {
                result = await _perkService.GetByIdAsync(id);

                if (!result.ISuccess)
                {
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePerk(int id, IFormCollection collection)
        {
            Result result = new();
            try
            {
                result = await _perkService.DeleteAsync(id);

                if (!result.ISuccess)
                {

                    return RedirectToAction("DeletePerk", id);
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
