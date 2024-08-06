
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Core.Application.Features.PropertyTypes.Commands.UpdatePropertyType
{
	/// <summary>
	/// Parameters for updating a property type
	/// </summary>
	public class UpdatePropertyTypeCommand : IRequest<Result<UpdatePropertyTypeDto>>
    {

		[SwaggerParameter(Description = "The id of the property type to update")]
		public int Id { get; set; }
		/// <example>
		/// House
		/// </example>
		[SwaggerParameter(Description = "The name of the property type to update")]
		public string Name { get; set; }
		/// <example>
		/// a sole property with loan
		/// </example>
		[SwaggerParameter(Description = "The description of the property type to update")]
		public string Description { get; set; }
    }

    public class UpdatePropertyTypeCommandHandler : IRequestHandler<UpdatePropertyTypeCommand, Result<UpdatePropertyTypeDto>>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;

        public UpdatePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<UpdatePropertyTypeDto>> Handle(UpdatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            return await BaseCqrsOperations.UpdateAsync<UpdatePropertyTypeCommand, UpdatePropertyTypeDto, PropertyType, int>(_propertyTypeRepository, _mapper, request.Id, request, "property type");
        }
    }
}
