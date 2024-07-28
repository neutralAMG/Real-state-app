

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Models.PropertyImgae;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Services.Persistance
{
    public class PropertyImageService : BaseService<PropertyImageModel, SavePropertyImageModel, PropertyImage, Guid>, IPropertyImageService
    {
        private readonly IPropertyImageRepository _propertyImageRepository;
        private readonly IMapper _mapper;

        public PropertyImageService(IPropertyImageRepository propertyImageRepository, IMapper mapper, string name = "Property Image") : base(propertyImageRepository, mapper, name)
        {
            _propertyImageRepository = propertyImageRepository;
            _mapper = mapper;
        }

        public Task<Result> UpdateAsync(string propertyId, string ImgUrl)
        {
            throw new NotImplementedException();
        }
    }
}
