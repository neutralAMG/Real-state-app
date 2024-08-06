using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Presentation.WebApp.Middleware.Filters;
using FinalProject.Presentation.WebApp.Models;
using FinalProject.Presentation.WebApp.Utils.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace Chequeando.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly ISelectListGenerator _selectListGenerator;
        private readonly ICheckBoxGenerator _checkBoxGenerator;
		private readonly IUserService _userService;

		public PropertyController(IPropertyService propertyService, ISelectListGenerator selectListGenerator, ICheckBoxGenerator checkBoxGenerator, IUserService userService)
        {
            _propertyService = propertyService;
            _selectListGenerator = selectListGenerator;
            _checkBoxGenerator = checkBoxGenerator;
			_userService = userService;
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
            if (id == default)
            {
                return NoContent();
            }
            Result<PropertyModel> result = new();
            try
            {
                result = await _propertyService.GetByIdAsync(id);

                if (!result.ISuccess)
                {
                    
                }
                var user = await _userService.GetByIdAsync(result.Data.AgentId);
                ViewBag.UserInfo = user.Data;
                return View(result.Data);
            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }
          
        }
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[Authorize(Roles = "Agent")]
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

		[ServiceFilter(typeof(IsUserNotLogIn))]
		[Authorize(Roles = "Agent")]
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
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("Create");
                }
                TempData["SuccessMessage"] = result.Message;

                return RedirectToAction("MantProperty", "Home");
            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }
    
        }

		[ServiceFilter(typeof(IsUserNotLogIn))]
		[Authorize(Roles = "Agent")]
		public async Task<IActionResult> EditProperty(Guid id)
        {
			if (id == default)
			{
				return NoContent();
			}
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

		[ServiceFilter(typeof(IsUserNotLogIn))]
		[Authorize(Roles = "Agent")]
        [ServiceFilter(typeof(IsThereSubEntitiesNeedItToCreateAProperty))]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProperty(Guid id, SavePropertyModel saveModel)
        {
			if (id == default || saveModel == null)
			{
				return NoContent();
			}
			Result<SavePropertyModel> result = new();
            try
            {
                result = await _propertyService.UpdateAsync(id, saveModel);

                if (!result.ISuccess)
                {
                    TempData["ErrorMessage"] = result.Message;
                    RedirectToAction("EditProperty", id);
                }

                TempData["SuccessMessage"] = result.Message;
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
                    TempData["ErrorMessage"] = result.Message;
                    return NoContent();
				}
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("Detail",new { id = result.Data.Id });

			}
			catch
			{
				return RedirectToAction("IndexAgent", "Home");
			}

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
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FavoriteProperty(Guid id, int IsMarktAsFavorite)
        {
            if (id == default || IsMarktAsFavorite == default)
            {
                return NoContent();
            }
            Result result = new();
            try
            {
                if(IsMarktAsFavorite == 1)  result = await _propertyService.HandlePropertyFavoriteState(id, true);
                if(IsMarktAsFavorite == 2)  result = await _propertyService.HandlePropertyFavoriteState(id, false);

         
                if (!result.ISuccess)
                {
                    TempData["ErrorMessage"] = result.Message;
                    return NoContent();
                }
                TempData["SuccessMessage"] = result.Message;
                return Redirect(Request.Headers["Referer"].ToString());
            }
            catch
            {
                return NoContent();
            }
            
        }
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[Authorize(Roles = "Agent")]
		public async Task<IActionResult> DeleteProperty(Guid id)
        {
            if (id == default)
            {
                return NoContent();
            }
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
		[ServiceFilter(typeof(IsUserNotLogIn))]
		[Authorize(Roles = "Agent")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteProperty(Guid id, bool fromFormMetadata)
        {
			if (id == default)
			{
				return NoContent();
			}
			Result result = new();
            try
            {
                result = await _propertyService.DeleteAsync(id);

                if (!result.ISuccess)
                {
                    TempData["ErrorMessage"] = result.Message;
                    return RedirectToAction("MantProperty", id);
                }
                TempData["SuccessMessage"] = result.Message;
                return RedirectToAction("IndexAgent", "Home");
            }
            catch
            {
                return RedirectToAction("IndexAgent", "Home");
            }
         
        }
    }
}
