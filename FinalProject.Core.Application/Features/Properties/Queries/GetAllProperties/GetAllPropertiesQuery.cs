

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.Properties.Queries.GetAllProperties
{
	/// <summary>
	/// Parameters for getting all the properties in the system
	/// </summary>
	public class GetAllPropertiesQuery : IRequest<Result<List<PropertyDto>>>
    {
    }

    public class GetAllPropertiesQueryHandler : IRequestHandler<GetAllPropertiesQuery, Result<List<PropertyDto>>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllPropertiesQueryHandler(IPropertyRepository propertyRepository,IUserRepository userRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<PropertyDto>>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            Result<List<PropertyDto>> result = await BaseCqrsOperations.GetAllAsync<PropertyDto, Property, Guid>(_propertyRepository, _mapper, "property");

            if (result.ISuccess)
            {
                foreach(PropertyDto property in result.Data)
                {
                    var user = await _userRepository.GetByIdAsync(property.AgentId);
                    property.AgentName = user == null ? "AgentName" : user.FirstName ;
                }
            }

            return result;
        }
    }
}
