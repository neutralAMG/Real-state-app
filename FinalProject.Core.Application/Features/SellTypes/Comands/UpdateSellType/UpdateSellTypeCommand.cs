
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.SellTypes.Comands.UpdateSellType
{
    public class UpdateSellTypeCommand : IRequest<Result<SaveSellTypeDto>>
    {
        public int Id { get; set; }
    }
    public class UpdateSellTypeCommandHandler : IRequestHandler<UpdateSellTypeCommand, Result<SaveSellTypeDto>>
    {
        private readonly ISellTypeRepository _sellTypeRepository;
        private readonly IMapper _mapper;

        public UpdateSellTypeCommandHandler(ISellTypeRepository sellTypeRepository, IMapper mapper)
        {
            _sellTypeRepository = sellTypeRepository;
            _mapper = mapper;
        }
        public async Task<Result<SaveSellTypeDto>> Handle(UpdateSellTypeCommand request, CancellationToken cancellationToken)
        {
            return await BaseCqrsOperations.UpdateAsync<UpdateSellTypeCommand, SaveSellTypeDto, SellType, int>(_sellTypeRepository, _mapper, request.Id, request, "sell type");
        }
    }
}
