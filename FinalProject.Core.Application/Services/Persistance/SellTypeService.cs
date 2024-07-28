
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Models.SellType;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Services.Persistance
{
    public class SellTypeService : BaseCompleteService<SellTypeModel, SaveSellTypeModel, SellType, int>, ISellTypeService
    {
        private readonly ISellTypeRepository _sellTypeRepository;
        private readonly IMapper _mapper;

        public SellTypeService(ISellTypeRepository sellTypeRepository, IMapper mapper, string name = "Sell Type") : base(sellTypeRepository, mapper, name)
        {
            _sellTypeRepository = sellTypeRepository;
            _mapper = mapper;
        }
    }
}
