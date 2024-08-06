

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Core.Application.Features.Properties.Queries.GetPropertyById
{
	/// <summary>
	/// Parameters for getting a propertyBy it's id
	/// </summary>
	public class GetPropertyByIdQuery : IRequest<Result<PropertyDto>>
    {
	
		[SwaggerParameter(Description = "The id of the property to get")]
		public Guid Id { get; set; }
    }

    public class GetPropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, Result<PropertyDto>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetPropertyByIdQueryHandler(IPropertyRepository propertyRepository,IUserRepository userRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<PropertyDto>> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
             Result<PropertyDto> result = await BaseCqrsOperations.GetByIdAsync<PropertyDto, Property, Guid>(_propertyRepository, _mapper, request.Id, "property");
            if (result.ISuccess)
            {
                var user = await _userRepository.GetByIdAsync(result.Data.AgentId);
                result.Data.AgentName = user == null ? "AgentName" : user.FirstName;
            }
            return result;
        }
    }
}
