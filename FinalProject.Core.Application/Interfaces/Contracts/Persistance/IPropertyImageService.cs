

using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Models.PropertyImgae;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Interfaces.Contracts.Persistance
{
    public interface IPropertyImageService : IBaseService<PropertyImageModel, SavePropertyImageModel, PropertyImage, Guid>
    {
        Task<Result> UpdateAsync(string propertyId, string ImgUrl);
    }
}
