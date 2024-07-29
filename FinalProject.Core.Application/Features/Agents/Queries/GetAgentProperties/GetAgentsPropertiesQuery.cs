
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Models.Property;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.Agents.Queries.GetAgentProperties
{
    public class GetAgentsPropertiesQuery : IRequest<Result<List<PropertyDto>>>
    {
        public string Id { get; set; }
    }

    public class GetAgentsPropertiesQueryHandler : IRequestHandler<GetAgentsPropertiesQuery, Result<List<PropertyDto>>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;

        public GetAgentsPropertiesQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<PropertyDto>>> Handle(GetAgentsPropertiesQuery request, CancellationToken cancellationToken)
        {
            return await GetAllAgentUserPropertiesAsync(request.Id);
        }

        private async Task<Result<List<PropertyDto>>> GetAllAgentUserPropertiesAsync(string id)
        {
            Result<List<PropertyDto>> result = new();
            try
            {
                List<Property> propertiesGetted = await _propertyRepository.GetAllCurrentAgentUserPropertiesAsync(id);

                result.Data = _mapper.Map<List<PropertyDto>>(propertiesGetted);
                result.Message = "Propertis where getted succesfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = "Critical error while getting the properties";
                return result;
            }
        }
    }
}
