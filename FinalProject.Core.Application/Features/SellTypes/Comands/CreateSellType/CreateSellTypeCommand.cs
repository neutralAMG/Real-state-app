
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.SellTypes.Comands.CreateSellType
{
    public class CreateSellTypeCommand : IRequest<Result<SaveSellTypeDto>>
    {
    }

    public class CreateSellTypeCommandHanler : IRequestHandler<CreateSellTypeCommand, Result<SaveSellTypeDto>>
    {
        private readonly ISellTypeRepository _sellTypeRepository;
        private readonly IMapper _mapper;

        public CreateSellTypeCommandHanler(ISellTypeRepository sellTypeRepository, IMapper mapper)
        {
            _sellTypeRepository = sellTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<SaveSellTypeDto>> Handle(CreateSellTypeCommand request, CancellationToken cancellationToken)
        {
            return await BaseCqrsOperations.SaveAsync<CreateSellTypeCommand, SaveSellTypeDto, SellType, int>(_sellTypeRepository, _mapper, request, "Sell Type" );
        }
    }
}
