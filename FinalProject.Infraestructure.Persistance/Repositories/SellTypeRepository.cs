
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;


namespace FinalProject.Infraestructure.Persistance.Repositories
{
    public class SellTypeRepository : BaseCompleteRepository<SellType, int>, ISellTypeRepository
    {
        private readonly FinalProjectContext _context;

        public SellTypeRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<List<SellType>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<SellType> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public override async Task<SellType> SaveAsync(SellType entity)
        {
            return await base.SaveAsync(entity);
        }

        public override async Task<SellType> UpdateAsync(SellType entity)
        {
            if (!await ExistsAsync(p => p.Id == entity.Id)) return null;
            SellType SellTypesToBeSaved = await _context.SellTypes.FindAsync(entity.Id);

            SellTypesToBeSaved.Name = entity.Name;

            return await base.UpdateAsync(SellTypesToBeSaved);
        }

        public override async Task<bool> DeleteAsync(int id)
        {

            if (!await ExistsAsync(P => P.Id == id)) return false;

            return await base.DeleteAsync(id);
        }

    }
}
