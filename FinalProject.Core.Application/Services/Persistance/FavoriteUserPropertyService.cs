

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
        private readonly IFavoriteUserPropertyRepository _favoriteUserPropertyRepository;

        internal FavoriteUserPropertyService(IFavoriteUserPropertyRepository favoriteUserPropertyRepository, IMapper mapper, string name = "image") : base(favoriteUserPropertyRepository, mapper, name)
        {
            _favoriteUserPropertyRepository = favoriteUserPropertyRepository;
        }

        public async Task<Result<FavoriteUserProperty>> GetByuserIdAndPropertyIdAsync(string userId, Guid propertyId)
        {
            Result<FavoriteUserProperty> result = new();
            try
            {
                FavoriteUserProperty entityGetted = await _favoriteUserPropertyRepository.GetByUserIdAndPropertyIdAsync(userId, propertyId);

                if (entityGetted == null)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while getting the property by it's Id";
                    return result;
                }

                result.Data = entityGetted;
                result.Message = $"The property get was a success";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critica error while getting the property by it's id ";
                return result;
            }
        }
    }
}
