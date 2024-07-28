

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Application.Utils.SessionHandler;
using FinalProject.Core.Domain.Entities;
using FinalProject.Core.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FinalProject.Core.Application.Services.Persistance
{
    public class PropertyService : BaseCompleteService<PropertyModel, SavePropertyModel, Property, Guid>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly string name;
        private readonly IFavoriteUserPropertyRepository _favoriteUserPropertyRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponce _currentUserInfo;
        private readonly SessionKeys _sessionKeys;

        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper,IFavoriteUserPropertyRepository favoriteUserPropertyRepository, IHttpContextAccessor httpContextAccessor, IOptions<SessionKeys> sessionKeys, string name = "Propeerty") : base(propertyRepository, mapper, name)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _favoriteUserPropertyRepository = favoriteUserPropertyRepository;
            _httpContextAccessor = httpContextAccessor;
            _sessionKeys = sessionKeys.Value;
            _currentUserInfo = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponce>(_sessionKeys.UserKey);
        }



        public async Task<Result<List<PropertyModel>>> FilterProperties(PropertyFilterModel filterModel)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<PropertyModel>>> GetAllCurrentAgentUserPropertiesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<PropertyModel>>> GetAllCurrentClientUserFavPropertiesAsync()
        {
            throw new NotImplementedException();
        }        
        public async Task<Result> AddPropertyToFavorite(string propertyId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> DeletePropertyFromFavorite(string propertyId)
        {
            throw new NotImplementedException();
        }
    }
}
