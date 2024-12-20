﻿

using FinalProject.Core.Application.Core;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Interfaces.Repositories.Persistance
{
    public interface IPropertyRepository : IBaseCompleteRepository<Property, Guid>
    {
        Task<List<Property>> GetAllCurrentAgentUserPropertiesAsync(string id);
        Task<List<Property>> GetAllCurrentClientUserFavPropertiesAsync(string id);
        Task<Property> GetByCodeAsync(string code);
        Task<List<Property>> GetAllWithCurrentClientLogIn(string id);
        Task<int> GetAmountOfPropertiesAsync();
    }
}
