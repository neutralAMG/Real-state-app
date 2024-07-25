
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

   

        public override async Task<PropertyPerk> SaveAsync(PropertyPerk entity)
        {
            return await base.SaveAsync(entity);
        }

        public virtual async Task<bool> DeleteAsync(PropertyPerk entity)
        {

            if (!await ExistsAsync(P => P.Id == entity.Id)) return false;

            PropertyPerk PropertyPerkToBeDeleted = await _context.PropertyPerks.FindAsync(entity.Id);

            return await base.DeleteAsync(PropertyPerkToBeDeleted);
        }

    }
}
