

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.PropertyTypes.Commands.DeletePropertyType
{
    public class DeletePropertyTypeCommand : IRequest<Result>
    {
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
