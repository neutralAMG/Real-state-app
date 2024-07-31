

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Interfaces.Utils;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Application.Models.PropertyImgae;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Services.Persistance
{
    internal class PropertyImageService : BaseService< SavePropertyImageModel, PropertyImage, Guid>, IPropertyImageService
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IFileHandler<Guid> _fileHandler;
        private readonly IMapper _mapper;
        private const string basePath = "/Images/Properties";
        public PropertyImageService(IPropertyImageRepository propertyImageRepository,IFileHandler<Guid> fileHandler, IMapper mapper, string name = "Property Image") : base(propertyImageRepository, mapper, name)
        {
            _propertyImageRepository = propertyImageRepository;
            _fileHandler = fileHandler;
            _mapper = mapper;
        }

        public override async Task<Result<SavePropertyImageModel>> SaveAsync(SavePropertyImageModel saveModel)
        {
             Result < SavePropertyImageModel > result = await base.SaveAsync(saveModel);

            if (!result.ISuccess) return result;
         
            saveModel.ImgUrl = _fileHandler.UploadFile(saveModel.file, basePath, result.Data.Id);
            saveModel.Id = result.Data.Id;
            Result updateOperation = await UpdateAsync(saveModel);

            if (!updateOperation.ISuccess)
            {
                result.ISuccess = false;
                result.Message = "Error saving the image";
                return result;
            }

            return result;
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

        public async Task<Result> UpdateAsync( SavePropertyImageModel updateModel)
        {
            Result result = new();
            try
            {
                updateModel.ImgUrl = _fileHandler.UpdateFile(updateModel.file, basePath, updateModel.ImgUrl, updateModel.Id);
                bool operation = await _propertyImageRepository.UpdateAsync(new PropertyImage {Id = updateModel.Id, PropertyId = updateModel.Propertyid, ImgUrl = updateModel.ImgUrl });
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
