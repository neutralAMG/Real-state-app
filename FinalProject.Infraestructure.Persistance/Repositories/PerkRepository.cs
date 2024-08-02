
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
        public override async Task<List<Perk>> GetAllAsync()
        {
            return await base.GetAllAsync();
        }

        public override async Task<Perk> GetByIdAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public override async Task<Perk> SaveAsync(Perk entity)
        {
            if (await ExistsAsync(e => e.Name == entity.Name)) return null;
            return await base.SaveAsync(entity);
        }

        public override async Task<Perk> UpdateAsync(Perk entity)
        {
            if (!await ExistsAsync(p => p.Id == entity.Id)) return null;

            Perk PerkToBeSaved = await _context.Perks.FindAsync(entity.Id);

            PerkToBeSaved.Name = entity.Name;
            PerkToBeSaved.Description = entity.Description;
 
            return await base.UpdateAsync(PerkToBeSaved);
        }

        public override async Task<bool> DeleteAsync(int id)
        {

            if (!await ExistsAsync(P => P.Id == id)) return false;

            return await base.DeleteAsync(id);
        }
   
    }
}
