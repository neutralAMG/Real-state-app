
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;


namespace FinalProject.Infraestructure.Persistance.Repositories
{
    internal class FavoriteUserPropertyRepository : BaseRepository<FavoriteUserProperty, Guid>, IFavoriteUserPropertyRepository
    {
        private readonly FinalProjectContext _context;

        public FavoriteUserPropertyRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }
  
        public override async Task<FavoriteUserProperty> SaveAsync(FavoriteUserProperty entity)
        {
            return await base.SaveAsync(entity);
        }

    
        public override async Task<bool> DeleteAsync(FavoriteUserProperty entity)
        {

            if (!await ExistsAsync(P => P.Id == entity.Id)) return false;

            FavoriteUserProperty FavoriteUserPropertyToBeDeleted = await _context.FavoriteUserProperties.FindAsync(entity.Id);

            return await base.DeleteAsync(FavoriteUserPropertyToBeDeleted);
        }
 
    }
}
