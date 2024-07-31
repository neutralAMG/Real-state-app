
using FinalProject.Core.Application.Core;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Interfaces.Repositories.Persistance
{
    public interface IPropertyImageRepository : IBaseRepository<PropertyImage,Guid>
    {
        Task<bool> UpdateAsync(PropertyImage entity);
        Task<List<Guid>> GetAllIdsByPropertyIdAsync(Guid propertyId);
    }
}
