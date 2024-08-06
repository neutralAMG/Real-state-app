

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Core.Application.Features.PropertyTypes.Commands.DeletePropertyType
{
	/// <summary>
	/// Parameters for deleting a property type by it's id
	/// </summary>
	public class DeletePropertyTypeCommand : IRequest<Result>
    {
		
		[SwaggerParameter(Description = "The id of the property type to be deleted")]
		public int Id { get; set; }
    }
    public class DeletePropertyTypeCommandHandler : IRequestHandler<DeletePropertyTypeCommand, Result>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
    

        public DeletePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository)
        {
            _propertyTypeRepository = propertyTypeRepository;
        }

        public async Task<Result> Handle(DeletePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            return await BaseCqrsOperations.DeleteAsync<PropertyType, int>(_propertyTypeRepository, request.Id, "property type");
        }
    }
}
