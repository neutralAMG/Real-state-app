
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;

namespace FinalProject.Infraestructure.Persistance.Repositories
{
    public class PropertyPerkRepository : BaseRepository<PropertyPerk, int>, IPropertyPerkRepository
    {
        private readonly FinalProjectContext _context;

        public PropertyPerkRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
