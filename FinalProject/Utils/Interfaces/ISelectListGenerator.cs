using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.Presentation.WebApp.Utils.Interfaces
{
    public interface ISelectListGenerator
    {
        Task<List<SelectListItem>> GeneratePropertyTypesSelectListAsync(int savedIdFromPropertyToUpdate = 0);
        Task<List<SelectListItem>> GenerateSellTypesSelectListAsync(int savedIdFromPropertyToUpdate = 0);


    }
}
