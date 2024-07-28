

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Models.FavoriteUserProperty;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Services.Persistance
{
    internal class FavoriteUserPropertyService : BaseService<SaveFavoriteUserPropertyModel, FavoriteUserProperty, int>, IFavoriteUserPropertyService
    {
        internal FavoriteUserPropertyService(IFavoriteUserPropertyRepository favoriteUserPropertyRepository, IMapper mapper, string name = "image") : base(favoriteUserPropertyRepository, mapper, name)
        {
        }

    }
}
