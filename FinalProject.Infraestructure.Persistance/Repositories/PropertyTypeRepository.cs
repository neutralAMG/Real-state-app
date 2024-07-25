

using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;

namespace FinalProject.Infraestructure.Persistance.Repositories
{
    public class PropertyTypeRepository : BaseCompleteRepository<PropertyType, int>
    {
        private readonly FinalProjectContext _context;

        public PropertyTypeRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
