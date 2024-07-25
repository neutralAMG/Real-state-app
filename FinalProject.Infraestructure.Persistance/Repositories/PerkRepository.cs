
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infraestructure.Persistance.Repositories
{
    public class PerkRepository : BaseCompleteRepository<Perk, int>, IPerkRepository
    {
        private readonly FinalProjectContext _context;

        public PerkRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }
        public override async Task<IList<Perk>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<Perk> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public override async Task<Perk> SaveAsync(Perk entity)
        {
            return await base.SaveAsync(entity);
        }

        public override async Task<Perk?> UpdateAsync(Perk entity)
        {
            if (!await ExistsAsync(p => p.Id == entity.Id)) return null;

            Perk PerkToBeSaved = await _context.Perks.FindAsync(entity.Id);

            PerkToBeSaved.Name = entity.Name;
 
            return await base.UpdateAsync(PerkToBeSaved);
        }

        public virtual async Task<bool> DeleteAsync(Perk entity)
        {

            if (!await ExistsAsync(P => P.Id == entity.Id)) return false;

            Perk PerkToBeDeleted = await _context.Perks.FindAsync(entity.Id);

            return await base.DeleteAsync(PerkToBeDeleted);
        }
   
    }
}
