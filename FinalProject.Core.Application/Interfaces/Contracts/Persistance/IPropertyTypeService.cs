

using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Models.PropertyType;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Interfaces.Contracts.Persistance
{
    public interface IPropertyTypeService : IBaseCompleteService<PropertyTypeModel, SavePropertyType, PropertyType, int>
    {
    }
}
