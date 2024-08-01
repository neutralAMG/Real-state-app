using FinalProject.Core.Application.Interfaces.Contracts.Persistance;

namespace FinalProject.Presentation.WebApp.Middleware.Validations
{
    public class PropertyValidations
    {
        private readonly IPropertyTypeService _propertyTypeService;
        private readonly IPerkService _perkService;
        private readonly ISellTypeService _sellTypeService;

        public PropertyValidations(IPropertyTypeService propertyTypeService, IPerkService perkService, ISellTypeService sellTypeService )
        {
            _propertyTypeService = propertyTypeService;
            _perkService = perkService;
            _sellTypeService = sellTypeService;
        }

        //refactor this piece of code
        public async Task<bool> IsTherePropertyTypesAvailableAsync()
        {
            var result = await _propertyTypeService.GetAllAsync();

            return result.Data.Any();
        }

        public async Task<bool> IsTherePerksAvailableAsync()
        {
            var result = await _perkService.GetAllAsync();

            return result.Data.Any();
        }

        public async Task<bool> IsThereSellTypesAvailableAsync()
        {
            var result = await _sellTypeService.GetAllAsync();

            return result.Data.Any();
        }
    }
}
