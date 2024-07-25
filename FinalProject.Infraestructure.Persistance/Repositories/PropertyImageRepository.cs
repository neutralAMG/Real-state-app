
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;

namespace FinalProject.Infraestructure.Persistance.Repositories
{
    public class PropertyImageRepository : BaseRepository<PropertyImage, int>, IPropertyImageRepository
    {
        private readonly FinalProjectContext _context;

        public PropertyImageRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
