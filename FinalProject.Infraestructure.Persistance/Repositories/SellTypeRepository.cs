

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
    }
}
