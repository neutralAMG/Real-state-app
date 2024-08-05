

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Interfaces.Utils;
using FinalProject.Core.Application.Models.PropertyImgae;
using FinalProject.Core.Domain.Entities;
using FinalProject.Core.Domain.Settings;
using Microsoft.Extensions.Options;

namespace FinalProject.Core.Application.Services.Persistance
{
    internal class PropertyImageService : BaseService< SavePropertyImageModel, PropertyImage, Guid>, IPropertyImageService
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IFileHandler<Guid> _fileHandler;
        private readonly IMapper _mapper;
        private readonly BasePathsForFileStorage _basePathsForFileStorage;
        public PropertyImageService(IPropertyImageRepository propertyImageRepository,IFileHandler<Guid> fileHandler, IOptions<BasePathsForFileStorage> basePaths,  IMapper mapper) : base(propertyImageRepository, mapper, "Property Image")
        {
            _propertyImageRepository = propertyImageRepository;
            _fileHandler = fileHandler;
            _mapper = mapper;
            _basePathsForFileStorage = basePaths.Value;
        }

        public override async Task<Result<SavePropertyImageModel>> SaveAsync(SavePropertyImageModel saveModel)
        {
            saveModel.ImgUrl = "Img";
             Result < SavePropertyImageModel > result = await base.SaveAsync(saveModel);

            if (!result.ISuccess) return result;
         
            saveModel.ImgUrl = await _fileHandler.UploadFile(saveModel.file, _basePathsForFileStorage.PropertyImagesBasePath, result.Data.Id);

            saveModel.Id = result.Data.Id;
            saveModel.file = null;

            Result updateOperation = await UpdateAsync(saveModel);

            if (!updateOperation.ISuccess)
            {
                result.ISuccess = false;
                result.Message = "Error saving the image";
                return result;
            }

            return result;
        }
        public override async Task<Result> DeleteAsync(Guid id)
        {
            Result result = new();

            _fileHandler.DeleteFile(_basePathsForFileStorage.PropertyImagesBasePath, id);
            result.Message = "Images delete wass successfull";
            return result;
        }
        public async Task<Result<List<Guid>>> GetAllIdsByPropertyIdAsync(Guid id)
        {
            Result<List<Guid>> result = new();
            try
            {
                List<Guid> imageGetted = await _propertyImageRepository.GetAllIdsByPropertyIdAsync(id);

                result.Data = imageGetted;

                if (imageGetted == null)
                {
                    result.ISuccess = false;
                    result.Message = "Error getting the image";
                    return result;
                }

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
                updateModel.ImgUrl = await _fileHandler.UpdateFile(updateModel.file, _basePathsForFileStorage.PropertyImagesBasePath, updateModel.ImgUrl, updateModel.Id);
                bool operation = await _propertyImageRepository.UpdateAsync(new PropertyImage {Id = updateModel.Id, PropertyId = updateModel.PropertyId, ImgUrl = updateModel.ImgUrl });
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
