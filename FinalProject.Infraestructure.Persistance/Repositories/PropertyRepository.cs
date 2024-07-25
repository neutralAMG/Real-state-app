using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;

namespace FinalProject.Infraestructure.Persistance.Repositories
{
    public class PropertyRepository : BaseCompleteRepository<Property, Guid>, IPropertyRepository
    {
        private readonly FinalProjectContext _context;

        public PropertyRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }

        public Task<List<Property>> GetAllCurrentAgentUserPropertiesAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Property>> GetAllCurrentClientUserFavPropertiesAsync(string id)
        {
            throw new NotImplementedException();
        }
    }
}
