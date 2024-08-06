
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using MediatR;

namespace FinalProject.Core.Application.Features.Agents.Queries.GetAllAgents
{
	/// <summary>
	/// Parameters for obtaining all the agent's in the system
	/// </summary>
	public class GetAllAgentsQuery : IRequest<Result<List<UserDto>>>
    {
    }
    public class GetAllAgentsQueryHandler : IRequestHandler<GetAllAgentsQuery, Result<List<UserDto>>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetAllAgentsQueryHandler(IUserRepository userRepository, IPropertyRepository propertyRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<UserDto>>> Handle(GetAllAgentsQuery request, CancellationToken cancellationToken)
        {
            return await GetAllAgentsRoleAsync();
        }
        private async Task<Result<List<UserDto>>> GetAllAgentsRoleAsync()
        {
            Result<List<UserDto>> result = new();
            try
            {
                List<GetUserDto> usersGetted = await _userRepository.GetAllBySpecificRoleAsync("Agent");

                result.Data = _mapper.Map<List<UserDto>>(usersGetted);

                if (result.Data is not null || result.Data.Count != 0) result.Data.ForEach(u => u.AmountOfProperties = _propertyRepository.GetAllCurrentAgentUserPropertiesAsync(u.Id).Result.Count);

                result.Message = "The user was getted successfully ";

                return result;

            }catch {
                result.ISuccess = false;
                result.Message = $"Critical error while getting the agent user's ";
                return result;
            }
        }
    }
}
