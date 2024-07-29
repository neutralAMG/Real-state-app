using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.SellTypes.Comands.DeleteSellType
{
    public class DeleteSellTypeCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }

    public class DeleteSellTypeCommandHandler : IRequestHandler<DeleteSellTypeCommand, Result>
    {
        private readonly ISellTypeRepository _sellTypeRepository;
        private readonly IMapper _mapper;

        public DeleteSellTypeCommandHandler(ISellTypeRepository sellTypeRepository, IMapper mapper)
        {
            _sellTypeRepository = sellTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(DeleteSellTypeCommand request, CancellationToken cancellationToken)
        {
            return await BaseCqrsOperations.DeleteAsync<SellType, int>(_sellTypeRepository, request.Id, "Sell type");
        }
    }
}
