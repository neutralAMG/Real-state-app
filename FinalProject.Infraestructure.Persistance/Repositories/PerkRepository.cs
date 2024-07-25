
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;

namespace FinalProject.Infraestructure.Persistance.Repositories
{
    public class PerkRepository : BaseCompleteRepository<Perk, int>, IPerkRepository
    {
        private readonly FinalProjectContext _context;

        public PerkRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
