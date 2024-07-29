using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;
using System.Security.Cryptography;

namespace FinalProject.Core.Application.Features.Perks.Querries.GetPerkById
{
    public class GetPerkByIdQuery : IRequest<Result<PerkDto>>
    {
        public int Id { get; set; }
    }

    public class GetPerkByIdQueryHandler : IRequestHandler<GetPerkByIdQuery, Result<PerkDto>>
    {
        private readonly IPerkRepository _perkRepository;
        private readonly IMapper _mapper;

        public GetPerkByIdQueryHandler(IPerkRepository perkRepository, IMapper mapper)
        {
            _perkRepository = perkRepository;
            _mapper = mapper;
        }
        public async Task<Result<PerkDto>> Handle(GetPerkByIdQuery request, CancellationToken cancellationToken)
        {
            Result<PerkDto> result = await GetByIdAsync(request.Id);
            return result;
        }

        public  async Task<Result<PerkDto>> GetByIdAsync(int id)
        {
            Result<PerkDto> result = new();
            try
            {
                Perk entityGetted = await _perkRepository.GetByIdAsync(id);

                if (entityGetted == null)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while getting the perk by it's Id";
                    return result;
                }

                result.Data = _mapper.Map<PerkDto>(entityGetted);
                result.Message = $"The perk get was a success";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critica error while getting the perk by it's id ";
                return result;
            }
        }
    }
}
