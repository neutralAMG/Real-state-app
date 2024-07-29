
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.Perks.Querries.GetAllPerks
{
    public class GetAllPerksQuery : IRequest<Result<List<PerkDto>>>
    {
    }

    public class GetAllPerksQueryHandler : IRequestHandler<GetAllPerksQuery, Result<List<PerkDto>>>
    {
        private readonly IPerkRepository _perkRepository;
        private readonly IMapper _mapper;

        public GetAllPerksQueryHandler(IPerkRepository perkRepository, IMapper mapper)
        {
            _perkRepository = perkRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<PerkDto>>> Handle(GetAllPerksQuery getAllPerksQuery, CancellationToken cancellationToken)
        {
            Result<List<PerkDto>> result = await GetAllAsync();
            return result;
        }
        private  async Task<Result<List<PerkDto>>> GetAllAsync()
        {
            Result<List<PerkDto>> result = new();
            try
            {
                List<Perk> entitesGetted = await _perkRepository.GetAllAsync();

                result.Data = _mapper.Map<List<PerkDto>>(entitesGetted);

                result.Message = $"The Perk's get was a success";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critica error while getting all the perk's";
                return result;
            }
        }
    }
}
