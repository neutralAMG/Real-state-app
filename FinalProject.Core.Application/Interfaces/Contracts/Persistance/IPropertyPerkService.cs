

using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Models.PropertyPerk;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Interfaces.Contracts.Persistance
{
    internal interface IPropertyPerkService : IBaseService<SavePropertyPerkModel, PropertyPerk, int>
    {
        Task<Result> UpdateAsync(List<int> perkIds, Guid propertyId);
    }
}
