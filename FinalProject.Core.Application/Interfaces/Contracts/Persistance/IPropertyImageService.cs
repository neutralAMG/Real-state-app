

using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Models.PropertyImgae;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Interfaces.Contracts.Persistance
{
    internal interface IPropertyImageService : IBaseService< SavePropertyImageModel, PropertyImage, Guid>
    {
        Task<Result> UpdateAsync(Guid propertyId, SavePropertyImageModel updateModel);
        Task<Result<PropertyImageModel>> GetByPropertyId(Guid propertyId);
    }
}
