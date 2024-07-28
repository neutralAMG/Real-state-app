
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Models.Property;

namespace FinalProject.Core.Application.Interfaces.Contracts.Persistance
{
    public interface IPropertyService : IBaseCompleteService<PropertyModel, SavePropertyModel, PropertyModel, Guid>
    {
        Task<Result<List<PropertyModel>>> GetAllCurrentAgentUserPropertiesAsync();
        Task<Result<List<PropertyModel>>> GetAllCurrentClientUserFavPropertiesAsync();
        Task<Result<List<PropertyModel>>> FilterProperties(PropertyFilterModel filterModel);
        Task<Result> AddPropertyToFavorite(string propertyId);
        Task<Result> DeletePropertyFromFavorite(string propertyId);
    }
}
