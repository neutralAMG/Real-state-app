using FinalProject.Presentation.WebApp.Models;

namespace FinalProject.Presentation.WebApp.Utils.Interfaces
{
    public interface ICheckBoxGenerator
    {
        Task<List<CheckBoxViewModel>> GeneratePerksCheckBoxOptionsAsync(List<int> perksIdInAUpdate = null);
    }
}
