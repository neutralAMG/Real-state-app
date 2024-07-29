
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.PropertyTypes.Commands.UpdatePropertyType
{
    public class UpdatePropertyTypeCommand : IRequest<Result<SavePropertyTypeDto>>
    {
        public int Id { get; set; }
    }

    public class UpdatePropertyTypeCommandHandler : IRequestHandler<UpdatePropertyTypeCommand, Result<SavePropertyTypeDto>>
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;
        private readonly IMapper _mapper;

        public UpdatePropertyTypeCommandHandler(IPropertyTypeRepository propertyTypeRepository, IMapper mapper)
        {
            _propertyTypeRepository = propertyTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<SavePropertyTypeDto>> Handle(UpdatePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            return await BaseCqrsOperations.UpdateAsync<UpdatePropertyTypeCommand, SavePropertyTypeDto, PropertyType, int>(_propertyTypeRepository, _mapper, request.Id, request, "property type");
        }
    }
}
