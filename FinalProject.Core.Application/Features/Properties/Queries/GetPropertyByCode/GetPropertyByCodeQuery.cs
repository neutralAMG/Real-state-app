using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Core.Application.Features.Properties.Queries.GetPropertyByCode
{
	/// <summary>
	/// Parameters for getting a property By it's code
	/// </summary>
	public class GetPropertyByCodeQuery : IRequest<Result<PropertyDto>>
    {
		/// <example>
		/// 3434242
		/// </example>
		[SwaggerParameter(Description = "The code of the property to get")]
		public string code { get; set; }
    }

    public class GetPropertyByCodeQueryHandler : IRequestHandler<GetPropertyByCodeQuery, Result<PropertyDto>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetPropertyByCodeQueryHandler(IPropertyRepository propertyRepository, IUserRepository userRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Result<PropertyDto>> Handle(GetPropertyByCodeQuery request, CancellationToken cancellationToken)
        {
            return await GetByCodeAsync(request.code);
        }


        private async Task<Result<PropertyDto>> GetByCodeAsync(string code)
        {
            Result<PropertyDto> result = new();
            try
            {
                Property entityGetted = await _propertyRepository.GetByCodeAsync(code);

                if (entityGetted == null)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while getting the property by it's Id";
                    return result;
                }

                result.Data = _mapper.Map<PropertyDto>(entityGetted);

                var user = await _userRepository.GetByIdAsync(entityGetted.AgentId);

                result.Data.AgentName = user == null ? "AgentName" : user.FirstName;

                result.Message = $"The property get was a success";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critica error while getting the property by it's id ";
                return result;
            }
        }
    }
}
