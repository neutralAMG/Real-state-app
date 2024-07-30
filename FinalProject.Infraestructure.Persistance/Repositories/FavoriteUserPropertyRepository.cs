
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infraestructure.Persistance.Repositories
{
    internal class FavoriteUserPropertyRepository : BaseRepository<FavoriteUserProperty, int>, IFavoriteUserPropertyRepository
    {
        private readonly FinalProjectContext _context;

        public FavoriteUserPropertyRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }
  
        public override async Task<FavoriteUserProperty> SaveAsync(FavoriteUserProperty entity)
        {
            if (await ExistsAsync(f => f.UserId == entity.UserId && f.PropertyId == entity.PropertyId)) return null;

            return await base.SaveAsync(entity);
        }

    
        public override async Task<bool> DeleteAsync(int id)
        {

            if (!await ExistsAsync(P => P.Id == id)) return false;

            FavoriteUserProperty FavoriteUserPropertyToBeDeleted = await _context.FavoriteUserProperties.FindAsync(id);

            return await base.DeleteAsync(id);
        }

        public async Task<FavoriteUserProperty> GetByUserIdAndPropertyIdAsync(string userId, Guid propertyId)
        {
            return await _context.FavoriteUserProperties.Where(f => f.UserId == userId && f.PropertyId == propertyId).FirstOrDefaultAsync();
        }
    }
}
