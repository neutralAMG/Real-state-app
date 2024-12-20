﻿

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Core.Application.Features.PropertyTypes.Queries.GetPropertyTypesById
{
	/// <summary>
	/// Parameters for getting a property by it's id
	/// </summary>
	public class GetPropertyTypeByIdQuery : IRequest<Result<PropertyTypeDto>>
    {
		
		[SwaggerParameter(Description = "The id of the property type to get")]
		public int Id { get; set; }
    }
    public class GetPropertyTypeByIdQueryHandler : IRequestHandler<GetPropertyTypeByIdQuery, Result<PropertyTypeDto>>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;

        public GetPropertyTypeByIdQueryHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<PropertyTypeDto>> Handle(GetPropertyTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await BaseCqrsOperations.GetByIdAsync<PropertyTypeDto, PropertyType, int>(_propertyTypeRepository, _mapper, request.Id, "property type");
        }
    }
}
