using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Presentation.WebApp.Models;
using FinalProject.Presentation.WebApp.Utils.Interfaces;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace Chequeando.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly ISelectListGenerator _selectListGenerator;
        private readonly ICheckBoxGenerator _checkBoxGenerator;

        public PropertyController(IPropertyService propertyService, ISelectListGenerator selectListGenerator, ICheckBoxGenerator checkBoxGenerator)
        {
            _propertyService = propertyService;
            _selectListGenerator = selectListGenerator;
            _checkBoxGenerator = checkBoxGenerator;
        }
        public async Task<IActionResult> Index()
        {
            Result<List<PropertyModel>> result = new();
            try{

                result = await _propertyService.GetAllAsync();

                if (!result.ISuccess)
                {

                }

                return View(result.Data);
            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }
          
        }

        public async Task<IActionResult> Detail(Guid id)
        {
            Result<PropertyModel> result = new();
            try
            {
                result = await _propertyService.GetByIdAsync(id);

                if (!result.ISuccess)
                {
                    
                }
                return View(result.Data);
            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }
          
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.PropertyTypes = await _selectListGenerator.GeneratePropertyTypesSelectListAsync();
            ViewBag.SellTypes = await _selectListGenerator.GenerateSellTypesSelectListAsync();
            ViewBag.Perks = await _checkBoxGenerator.GeneratePerksCheckBoxOptionsAsync();
            var model = new SavePropertyViewModel
            {
                SavePropertyModel = new SavePropertyModel(),
                perks = await _checkBoxGenerator.GeneratePerksCheckBoxOptionsAsync(),
                PropertyTypes = await _selectListGenerator.GeneratePropertyTypesSelectListAsync(),
                SellTypes = await _selectListGenerator.GenerateSellTypesSelectListAsync(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SavePropertyModel saveModel)
        {
            Result<SavePropertyModel> result = new();
            try
            {
                result = await _propertyService.SaveAsync(saveModel);

                if (!result.ISuccess)
                {

                }
                return RedirectToAction("IndexAgent", "Home");
            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }
    
        }

        public async Task<IActionResult> EditProperty(Guid id)
        {
            Result<PropertyModel> result = new();
            try
            {
                result = await _propertyService.GetByIdAsync(id);

                if (!result.ISuccess)
                {

                }

                var model = new SavePropertyViewModelWithData
                {
                    SavePropertyModel = result.Data,
                    perks = await _checkBoxGenerator.GeneratePerksCheckBoxOptionsAsync(result.Data.PropertyPerks.Select(p => p.Id).ToList()),
                    PropertyTypes = await _selectListGenerator.GeneratePropertyTypesSelectListAsync(result.Data.PropertyTypeName),
                    SellTypes = await _selectListGenerator.GenerateSellTypesSelectListAsync(result.Data.SellTypeName)
                };
                return View(model);
            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProperty(Guid id, SavePropertyModel saveModel)
        {
            Result<SavePropertyModel> result = new();
            try
            {
                result = await _propertyService.UpdateAsync(id, saveModel);

                if (!result.ISuccess)
                {
                    RedirectToAction("EditProperty", id);
                }

                return RedirectToAction("IndexAgent", "Home");

            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }
         
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> SearchByCode(string code)
		{
            if(code == default)
            {
                return NoContent();
            }
			Result<PropertyModel> result = new();
			try
			{
				result = await _propertyService.GetByCodeAsync(code);

				if (!result.ISuccess)
				{
                    return NoContent();
				}

				return View("Detail", result.Data);

			}
			catch
			{
				return RedirectToAction("IndexAgent", "Home");
			}

		}


		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FavoriteProperty(Guid id, bool IsMarktAsFavorite)
        {
            Result result = new();
            try
            {
                result = await _propertyService.HandlePropertyFavoriteState(id, IsMarktAsFavorite);
                return default;
            }
            catch
            {
                throw;
            }
            
        }

        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            Result<PropertyModel> result = new();
            try
            {
                result = await _propertyService.GetByIdAsync(id);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProperty(Guid id, bool fromFormMetadata)
        {
            Result result = new();
            try
            {
                result = await _propertyService.DeleteAsync(id);

                if (!result.ISuccess)
                {
                    return RedirectToAction("DeleteProperty", id);
                }

                return RedirectToAction("IndexAgent", "Home");
            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }
         
        }
    }
}
