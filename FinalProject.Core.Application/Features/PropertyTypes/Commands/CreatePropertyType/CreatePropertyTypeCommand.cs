
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Core.Application.Features.PropertyTypes.Commands.CreatePropertyType
{
	/// <summary>
	/// Parameters for saving a new property type 
	/// </summary>
	public class CreatePropertyTypeCommand : IRequest<Result<SavePropertyTypeDto>>
    {
		/// <example>
		/// appartment
		/// </example>
		[SwaggerParameter(Description = "The name of the property type to save")]
		public string Name { get; set; }
		/// <example>
		/// This property is part of a building
		/// </example>
		[SwaggerParameter(Description = "The description of the property type to save")]
		public string Description { get; set; }
    }
    public class CreatePropertyTypeCommandHandler : IRequestHandler<CreatePropertyTypeCommand, Result<SavePropertyTypeDto>>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;

        public CreatePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<SavePropertyTypeDto>> Handle(CreatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            return await BaseCqrsOperations.SaveAsync<CreatePropertyTypeCommand, SavePropertyTypeDto, PropertyType, int>(_propertyTypeRepository, _mapper, request, "Property type");
        }
    }
}
