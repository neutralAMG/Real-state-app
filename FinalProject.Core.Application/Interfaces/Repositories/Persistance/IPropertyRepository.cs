

using FinalProject.Core.Application.Core;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Interfaces.Repositories.Persistance
{
    public interface IPropertyRepository : IBaseCompleteRepository<Property, Guid>
    {
        Task<List<Property>> GetAllCurrentAgentUserPropertiesAsync(string id);
        Task<List<Property>> GetAllCurrentClientUserFavPropertiesAsync(string id);
    }
}
