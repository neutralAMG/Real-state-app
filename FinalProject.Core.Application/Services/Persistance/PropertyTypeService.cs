

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Models.PropertyType;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Services.Persistance
{
    public class PropertyTypeService : BaseCompleteService<PropertyTypeModel, SavePropertyTypeModel, PropertyType, int>, IPropertyTypeService
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;

        public PropertyTypeService(IPropertyTypeRepository propertyTypeRepository, IMapper mapper, string name = "Property type") : base(propertyTypeRepository, mapper, name)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }
    }
}
