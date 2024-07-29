

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Enums;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Interfaces.Utils;
using FinalProject.Core.Application.Models.FavoriteUserProperty;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Application.Models.PropertyImgae;
using FinalProject.Core.Application.Utils.PropertyFilters;
using FinalProject.Core.Application.Utils.SessionHandler;
using FinalProject.Core.Domain.Entities;
using FinalProject.Core.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace FinalProject.Core.Application.Services.Persistance
{
    public class PropertyService : BaseCompleteService<PropertyModel, SavePropertyModel, Property, Guid>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly IFavoriteUserPropertyService _favoriteUserPropertyService;
        private readonly IPropertyImageService _propertyImageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileHandler<Guid> _fileHandler;
        private readonly AuthenticationResponce _currentUserInfo;
        private readonly SessionKeys _sessionKeys;
        private const string basePath = "/Images/Properties";

        //Todo: Make it so the peks can be cass a a list and update acordently
        //Todo: The get by id should also get the agent related to it
        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper, IServiceProvider service, IHttpContextAccessor httpContextAccessor, IOptions<SessionKeys> sessionKeys, IFileHandler<Guid> fileHandler, string name = "Propeerty") : base(propertyRepository, mapper, name)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _favoriteUserPropertyService = service.GetRequiredService<IFavoriteUserPropertyService>();
            _propertyImageService = service.GetRequiredService<IPropertyImageService>();
            _httpContextAccessor = httpContextAccessor;
            _fileHandler = fileHandler;
            _sessionKeys = sessionKeys.Value;
            _currentUserInfo = httpContextAccessor.HttpContext.Session.Get<AuthenticationResponce>(_sessionKeys.UserKey);
        }

        public override async Task<Result<List<PropertyModel>>> GetAllAsync()
        {
            if (_currentUserInfo != null && _currentUserInfo.Roles.Any(u => u == "Client"))
            {
                return await GetAllWithCurrentClientLogIn();
            }
            return await base.GetAllAsync();
        }
        public override async Task<Result<SavePropertyModel>> SaveAsync(SavePropertyModel saveModel)
        {
            Result<SavePropertyModel> result = await base.SaveAsync(saveModel);

            if (result.ISuccess)
            {
                saveModel.PropertyImagesFiles.ForEach(async i =>
                    {
                        await _propertyImageService.SaveAsync(new SavePropertyImageModel
                        {
                            ImgUrl = _fileHandler.UploadFile(i, basePath, result.Data.Id),
                            Propertyid = result.Data.Id
                        });

                    });
            }
            return result;
        }
        public virtual async Task<Result<SavePropertyModel>> UpdateAsync(Guid id, SavePropertyModel updateModel)
        {
            Result<SavePropertyModel> result = await base.UpdateAsync(id, updateModel);

            if (result.ISuccess)
            {
                updateModel.PropertyImagesFiles.ForEach(async i =>
                {

                    var propertyToUbdate = await _propertyImageService.GetByPropertyId(id);

                    await _propertyImageService.UpdateAsync(id, new SavePropertyImageModel
                    {
                        ImgUrl = _fileHandler.UpdateFile(i, basePath, propertyToUbdate.Data.ImgUrl, id),
                        Propertyid = result.Data.Id
                    });

                });
            }
            return result;
        }
        public async Task<Result<List<PropertyModel>>> FilterProperties(PropertyFilterModel filterModel)
        {
            Result<List<PropertyModel>> result = new();
            try
            {
                List<Property> propertyGetted = await _propertyRepository.GetAllAsync();

                propertyGetted = PropertyFilters.FilterProperties(propertyGetted, filterModel).ToList();

                result.Data = _mapper.Map<List<PropertyModel>>(propertyGetted);

                result.Message = "Properties where filtered successfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = "Critical error while filtering the properties";
                return result;
            }
        }

        public async Task<Result<List<PropertyModel>>> GetAllCurrentAgentUserPropertiesAsync()
        {
            Result<List<PropertyModel>> result = new();
            try
            {
                List<Property> propertiesGetted = await _propertyRepository.GetAllCurrentAgentUserPropertiesAsync(_currentUserInfo.Id);

                result.Data = _mapper.Map<List<PropertyModel>>(propertiesGetted);
                result.Message = "Propertis where getted succesfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = "Critical error while getting the properties";
                return result;
            }
        }

        public async Task<Result<List<PropertyModel>>> GetAllCurrentClientUserFavPropertiesAsync()
        {
            Result<List<PropertyModel>> result = new();
            try
            {
                List<Property> propertiesGetted = await _propertyRepository.GetAllCurrentClientUserFavPropertiesAsync(_currentUserInfo.Id);

                List<PropertyModel> propertiesMapped = _mapper.Map<List<PropertyModel>>(propertiesGetted);

                propertiesMapped.ForEach(p => p.IsMarkAsFavoriteByCurrentUser = true);

                result.Data = propertiesMapped;

                result.Message = "Properties where getted successfully";

                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = "Critical error while getting the current client favorite properties";
                return result;
            }
        }
        public async Task<Result> HandlePropertyFavoriteState(FavoriteUserPropertyModel model, bool isMarkFavoriteByUser)
        {
            Result result = new();
            string operationToBeHandled = isMarkFavoriteByUser ? "add" : "Delet";
            try
            {

                if (!isMarkFavoriteByUser)
                {
                    result = await _favoriteUserPropertyService.SaveAsync(new SaveFavoriteUserPropertyModel { UserId = _currentUserInfo.Id, PropertyId = model.PropertyId });

                }
                result = await _favoriteUserPropertyService.DeleteAsync(model.Id);

                result.Message = $"Property {operationToBeHandled}ed to favorites";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical error while trying {operationToBeHandled} the property";
                return result;
            }
        }


        private async Task<Result<List<PropertyModel>>> GetAllWithCurrentClientLogIn()
        {
            Result<List<PropertyModel>> result = new();
            try
            {
                List<Property> propertiesGetted = await _propertyRepository.GetAllWithCurrentClientLogIn(_currentUserInfo.Id);

                List<PropertyModel> propertiesMapped = _mapper.Map<List<PropertyModel>>(propertiesGetted);

                IEnumerable<Guid> userFavPropertiesIds = propertiesGetted.SelectMany(p => p.FavoriteUsersProperties.Select(p => p.PropertyId));

                propertiesMapped.ForEach(p =>
                {
                    if (userFavPropertiesIds.Contains(p.Id)) p.IsMarkAsFavoriteByCurrentUser = true;
                }
                );

                result.Data = propertiesMapped;

                result.Message = "Properties where getted successfully";

                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = "Critical error while getting the current client favorite properties";
                return result;
            }
        }

        public async Task<Result<PropertyModel>> GetByCodeAsync(string code)
        {
            Result<PropertyModel> result = new();
            try
            {
                Property entityGetted = await _propertyRepository.GetByCodeAsync(code);

                if (entityGetted == null)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while getting the property by it's Id";
                    return result;
                }

                result.Data = _mapper.Map<PropertyModel>(entityGetted);
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
