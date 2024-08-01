
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Enums;
using FinalProject.Core.Application.Models.FavoriteUserProperty;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Interfaces.Contracts.Persistance
{
    public interface IPropertyService : IBaseCompleteService<PropertyModel, SavePropertyModel, Property, Guid>
    {
        Task<Result<List<PropertyModel>>> GetAllCurrentAgentUserPropertiesAsync();
        Task<Result<List<PropertyModel>>> GetAllCurrentClientUserFavPropertiesAsync();
        Task<Result<List<PropertyModel>>> FilterProperties(PropertyFilterModel filterModel);
        Task<Result<PropertyModel>> GetByCodeAsync(string code);
        Task<Result> HandlePropertyFavoriteState(Guid propertyId, bool isMarkFavoriteByUser);
    }
}
