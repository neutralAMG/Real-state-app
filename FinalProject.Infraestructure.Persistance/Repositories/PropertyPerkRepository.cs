
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infraestructure.Persistance.Repositories
{
    public class PropertyPerkRepository : BaseRepository<PropertyPerk, int>, IPropertyPerkRepository
    {
        private readonly FinalProjectContext _context;

        public PropertyPerkRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<PropertyPerk> SaveAsync(PropertyPerk entity)
        {
            return await base.SaveAsync(entity);
        }

        public override async Task<bool> DeleteAsync(int id)
        {

            if (!await ExistsAsync(P => P.Id == id)) return false;


            return await base.DeleteAsync(id);
        }

        public async Task<List<PropertyPerk>> GetAllPerksIdByPropertyId(Guid id)
        {
            return await _context.PropertyPerks.Where(p => p.PropertyId == id).ToListAsync();
        }
    }
}
