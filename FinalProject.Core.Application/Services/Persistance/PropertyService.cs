

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Models.FavoriteUserProperty;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Application.Models.PropertyImgae;
using FinalProject.Core.Application.Models.PropertyPerk;
using FinalProject.Core.Application.Utils.CodeGenerator;
using FinalProject.Core.Application.Utils.PropertyFilters;
using FinalProject.Core.Application.Utils.SessionHandler;
using FinalProject.Core.Domain.Entities;
using FinalProject.Core.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace FinalProject.Core.Application.Services.Persistance
{ 
    public class PropertyService : BaseCompleteService<PropertyModel, SavePropertyModel, Property, Guid>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly IFavoriteUserPropertyService _favoriteUserPropertyService;
        private readonly IPropertyImageService _propertyImageService;
        private readonly IPropertyPerkService _propertyPerkService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponce _currentUserInfo;
        private readonly SessionKeys _sessionKeys;


        //Todo: The get by id should also get the agent related to it
        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper, IServiceProvider service, IHttpContextAccessor httpContextAccessor, IOptions<SessionKeys> sessionKeys) : base(propertyRepository, mapper, "Property")
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
            _favoriteUserPropertyService = service.GetRequiredService<IFavoriteUserPropertyService>();
            _propertyImageService = service.GetRequiredService<IPropertyImageService>();
            _propertyPerkService = service.GetRequiredService<IPropertyPerkService>();
            _httpContextAccessor = httpContextAccessor;
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
            saveModel.PropertyCode = PropertyCodeGenerator.GeneratePropertyCode();

            Result<SavePropertyModel> result = await base.SaveAsync(saveModel);

            if (result.ISuccess)
            {
                foreach (KeyValuePair<string, IFormFile> file in saveModel.ImagesToUpdateAndItsFiles)
                {
                    if (file.Value is null) continue;
                    await _propertyImageService.SaveAsync(new SavePropertyImageModel
                    {
                        Propertyid = result.Data.Id,
                        file = file.Value,
                    });
                }

                foreach (int id in saveModel.PropertyPerks)
                {
                    await _propertyPerkService.SaveAsync(new SavePropertyPerkModel
                    {
                        PerkId = id,
                        PropertyId = result.Data.Id
                    });
                }
            }
            return result;
        }
        public override async Task<Result> DeleteAsync(Guid id)
        {
            Result<List<Guid>> propertyImagesIdsToBeDeleted = await _propertyImageService.GetAllIdsByPropertyIdAsync(id);
            Result result = await base.DeleteAsync(id);
            if (result.ISuccess)
            {
                foreach (Guid imageId in propertyImagesIdsToBeDeleted.Data)
                {
                    await _propertyImageService.DeleteAsync(imageId);
                }

            }
            return result;
        }
        public virtual async Task<Result<SavePropertyModel>> UpdateAsync(Guid id, SavePropertyModel updateModel)
        {

            Result<SavePropertyModel> result = await base.UpdateAsync(id, updateModel);

            if (result.ISuccess)
            {
                foreach (KeyValuePair<string, IFormFile> ImageToBeUpdated in updateModel.ImagesToUpdateAndItsFiles)
                {
                    if (ImageToBeUpdated.Value == null) continue;

                    await _propertyImageService.UpdateAsync(new SavePropertyImageModel
                    {
                        //Get's the entity (Property Image) primary key. for more info go to File handler in the Utils folder and the save method in PropertyImage service
                        Id = Guid.Parse(ImageToBeUpdated.Key.Split("/")[3].ToUpper()),
                        ImgUrl = ImageToBeUpdated.Key,
                        Propertyid = id,
                        file = ImageToBeUpdated.Value,
                    });
                }
                await _propertyPerkService.UpdateAsync(updateModel.PropertyPerks, id);

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

            if (_currentUserInfo == null)
            {
                result.ISuccess = false;
                result.Message = "There is no user logIn right now";
                return result;
            }
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

            if (_currentUserInfo == null)
            {
                result.ISuccess = false;
                result.Message = "There is no user logIn right now";
                return result;
            }
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
        public async Task<Result> HandlePropertyFavoriteState(Guid propertyId, bool isMarkFavoriteByUser)
        {
            Result result = new();
            string operationToBeHandled = isMarkFavoriteByUser ? "add" : "Delet";

            if (_currentUserInfo == null)
            {
                result.ISuccess = false;
                result.Message = "There is no user logIn right now";
                return result;
            }
            try
            {

                if (!isMarkFavoriteByUser)
                {
                    result = await _favoriteUserPropertyService.SaveAsync(new SaveFavoriteUserPropertyModel { UserId = _currentUserInfo.Id, PropertyId = propertyId });

                }
                //refactor this part of code
                var entityToBeDeleted = await _favoriteUserPropertyService.GetByuserIdAndPropertyIdAsync(_currentUserInfo.Id, propertyId);
                result = await _favoriteUserPropertyService.DeleteAsync(entityToBeDeleted.Data.Id);

                result.Message = $"Property {operationToBeHandled}ed to favorites";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical error while trying to {operationToBeHandled} the property";
                return result;
            }
        }


        private async Task<Result<List<PropertyModel>>> GetAllWithCurrentClientLogIn()
        {
            Result<List<PropertyModel>> result = new();

            if (_currentUserInfo == null)
            {
                result.ISuccess = false;
                result.Message = "There is no user logIn right now";
                return result;
            }

            try
            {
                List<Property> propertiesGetted = await _propertyRepository.GetAllWithCurrentClientLogIn(_currentUserInfo.Id);

                List<PropertyModel> propertiesMapped = _mapper.Map<List<PropertyModel>>(propertiesGetted);

                IEnumerable<Guid> userFavPropertiesIds = propertiesGetted.SelectMany(p => p.FavoriteUsersProperties.Select(p => p.PropertyId));

                propertiesMapped.ForEach(p =>
                {
                    if (userFavPropertiesIds.Any(up => up == p.Id)) p.IsMarkAsFavoriteByCurrentUser = true;
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

        public async Task<Result<List<PropertyModel>>> GetSpecificAgentProperties(string id)
        {
            Result<List<PropertyModel>> result = new();

           
            try
            {
                List<Property> propertiesGetted = await _propertyRepository.GetAllCurrentAgentUserPropertiesAsync(id);

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
    }
}
