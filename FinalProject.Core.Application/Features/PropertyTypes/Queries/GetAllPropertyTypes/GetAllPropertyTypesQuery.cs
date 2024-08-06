
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.PropertyTypes.Queries.GetAllPropertyTypes
{
	/// <summary>
	/// Parameters for getting all the properties in the system
	/// </summary>
	public class GetAllPropertyTypesQuery : IRequest<Result<List<PropertyTypeDto>>>
    {
    }

    public class GetAllPropertyTypesQueryHandler : IRequestHandler<GetAllPropertyTypesQuery, Result<List<PropertyTypeDto>>>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;

        public GetAllPropertyTypesQueryHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<PropertyTypeDto>>> Handle(GetAllPropertyTypesQuery request, CancellationToken cancellationToken)
        {
            return await BaseCqrsOperations.GetAllAsync<PropertyTypeDto, PropertyType, int>(_propertyTypeRepository, _mapper, "property type");
        }
    }
}
