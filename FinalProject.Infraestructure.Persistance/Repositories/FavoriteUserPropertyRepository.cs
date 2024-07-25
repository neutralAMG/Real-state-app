

using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Core;

namespace FinalProject.Infraestructure.Persistance.Repositories
{
    internal class FavoriteUserPropertyRepository : BaseRepository<FavoriteUserProperty, int>, IFavoriteUserPropertyRepository
    {
        private readonly FinalProjectContext _context;

        public FavoriteUserPropertyRepository(FinalProjectContext context) : base(context)
        {
            _context = context;
        }
    }
}
