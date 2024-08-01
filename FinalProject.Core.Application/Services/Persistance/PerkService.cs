
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Models.Perk;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Services.Persistance
{
    public class PerkService : BaseCompleteService<PerkModel, SavePerkModel, Perk, int>, IPerkService
    {
        private readonly IPerkRepository _perkRepository;
        private readonly IMapper _mapper;

        public PerkService(IPerkRepository perkRepository, IMapper mapper) : base(perkRepository, mapper, "Perk")
        {
            _perkRepository = perkRepository;
            _mapper = mapper;
        }
    }
}
