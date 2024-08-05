using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Presentation.WebApp.Utils.Interfaces
{
    public interface ISelectListGenerator
    {
        Task<List<SelectListItem>> GeneratePropertyTypesSelectListAsync(string savedIdFromPropertyToUpdate = null);
        Task<List<SelectListItem>> GenerateSellTypesSelectListAsync(string savedIdFromPropertyToUpdate = null);


    }
}
