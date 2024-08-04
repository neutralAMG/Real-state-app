﻿
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.SellTypes.Comands.UpdateSellType
{
    public class UpdateSellTypeCommand : IRequest<Result<UpdateSellTypeDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UpdateSellTypeCommandHandler : IRequestHandler<UpdateSellTypeCommand, Result<UpdateSellTypeDto>>
    {
        private readonly ISellTypeRepository _sellTypeRepository;
        private readonly IMapper _mapper;

        public UpdateSellTypeCommandHandler(ISellTypeRepository sellTypeRepository, IMapper mapper)
        {
            _sellTypeRepository = sellTypeRepository;
            _mapper = mapper;
        }
        public async Task<Result<UpdateSellTypeDto>> Handle(UpdateSellTypeCommand request, CancellationToken cancellationToken)
        {
            return await BaseCqrsOperations.UpdateAsync<UpdateSellTypeCommand, UpdateSellTypeDto, SellType, int>(_sellTypeRepository, _mapper, request.Id, request, "sell type");
        }
    }
}
