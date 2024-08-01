using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Models.Perk;
using FinalProject.Presentation.WebApp.Models;
using FinalProject.Presentation.WebApp.Utils.Interfaces;

namespace FinalProject.Presentation.WebApp.Utils.WebbAppCheckBoxGenerator
{
    public class CheckBoxGenerator : ICheckBoxGenerator
    {
        private readonly IPerkService _perkService;

        public CheckBoxGenerator(IPerkService perkService)
        {
            _perkService = perkService;
        }

        public async Task<List<CheckBoxViewModel>> GeneratePerksCheckBoxOptionsAsync(List<int> perksIdInAnUpdate = null)
        {
            Result<List<PerkModel>> perkResult = await _perkService.GetAllAsync();

            HashSet<int> perkIds = perksIdInAnUpdate is not null ? perksIdInAnUpdate.ToHashSet() : null;

            List<CheckBoxViewModel> checkBoxs = perkResult.Data.Select(p => new CheckBoxViewModel
            {
                Name = p.Name,
                Value = p.Id,
                IsSelected = perkIds is not null && perkIds.Contains(p.Id)

            }).ToList();
         
            return checkBoxs;
        }
    }
}
