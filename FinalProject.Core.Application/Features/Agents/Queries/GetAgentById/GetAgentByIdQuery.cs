

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Models.User;
using FinalProject.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Core.Application.Features.Agents.Queries.GetAgentById
{
	/// <summary>
	/// Parameters to get an agent by it's id
	/// </summary>
	public class GetAgentByIdQuery : IRequest<Result<UserDto>>
    {
		[SwaggerParameter(Description = "The user id used to obtain a agent")]
		public string Id { get; set; }
    }

    public class GetAgentByIdQueryHandler : IRequestHandler<GetAgentByIdQuery, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetAgentByIdQueryHandler(IUserRepository userRepository, IPropertyRepository propertyRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<Result<UserDto>> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
        {
            return await GetByIdAsync(request.Id);
        }
        private async Task<Result<UserDto>> GetByIdAsync(string id)
        {
            Result<UserDto> result = new();
            try
            {
                if (id == null)
                {
                    result.ISuccess = false;
                    result.Message = "The id cant be empty";
                    return result;
                }

                GetUserDto userGetted = await _userRepository.GetByIdAsync(id);

                List<Property> properties = await _propertyRepository.GetAllCurrentAgentUserPropertiesAsync(id);

                if (userGetted == null)
                {
                    result.ISuccess = false;
                    result.Message = "Error getting the user";
                    return result;
                }

                result.Data = _mapper.Map<UserDto>(userGetted);

                result.Data.AmountOfProperties = properties.Count;

                result.Message = "The user was getted susccessfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = "Critical error while getting the user";
                return result;
            }
        }
    }
}
