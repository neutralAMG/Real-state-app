

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Application.Models.PropertyImgae;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Services.Persistance
{
    internal class PropertyImageService : BaseService< SavePropertyImageModel, PropertyImage, Guid>, IPropertyImageService
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;

        public PropertyImageService(IPropertyImageRepository propertyImageRepository, IMapper mapper, string name = "Property Image") : base(propertyImageRepository, mapper, name)
        {
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
        }

        public async Task<Result<PropertyImageModel>> GetByIdAsync(Guid id)
        {
            Result<PropertyImageModel> result = new();
            try
            {
                PropertyImage imageGetted = await _propertyImageRepository.GetByIdAsync(id);


                if (imageGetted == null)
                {
                    result.ISuccess = false;
                    result.Message = "Error getting the image";
                    return result;
                }

                result.Data = _mapper.Map<PropertyImageModel>(imageGetted);

                result.Message = "The Image was getted successfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = "Critical error while trying to get the image";
                return result;
            }
        }

        public async Task<Result> UpdateAsync(Guid propertyId, SavePropertyImageModel updateModel)
        {
            Result result = new();
            try
            {
                bool operation = await _propertyImageRepository.UpdateAsync(new PropertyImage { PropertyId = propertyId, ImgUrl = updateModel.ImgUrl });
                if (!operation) {
                    result.ISuccess = false;
                    result.Message = "Error updating the image";
                    return result;
                }

                result.Message = "The Image was updated successfully";
                return result;
            }
            catch
            {
                result.ISuccess= false;
                result.Message = "Critical error while trying to update the image";
                return result;
            }
        }
    }
}
