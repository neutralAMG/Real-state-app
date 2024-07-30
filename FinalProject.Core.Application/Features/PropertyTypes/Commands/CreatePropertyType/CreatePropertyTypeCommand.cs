
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.PropertyTypes.Commands.CreatePropertyType
{
    public class CreatePropertyTypeCommand : IRequest<Result<SavePropertyTypeDto>>
    {
        public string Name { get; set; }
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
