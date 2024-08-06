

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.SellTypes.Queries.GetAllSellTypes
{
	/// <summary>
	/// Parameters for getting all the sale types in the system 
	/// </summary>
	public class GetAllSellTypesQuery : IRequest<Result<List<SellTypeDto>>>
    {
    }
    public class GetAllSellTypesQueryHandler : IRequestHandler<GetAllSellTypesQuery, Result<List<SellTypeDto>>>
    {
        private readonly ISellTypeRepository _sellTypeRepository;
        private readonly IMapper _mapper;

        public GetAllSellTypesQueryHandler(ISellTypeRepository sellTypeRepository, IMapper mapper)
        {
            _sellTypeRepository = sellTypeRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<SellTypeDto>>> Handle(GetAllSellTypesQuery request, CancellationToken cancellationToken)
        {
            return await BaseCqrsOperations.GetAllAsync<SellTypeDto,SellType, int>(_sellTypeRepository, _mapper, "sell type");
        }
        private async Task<Result<List<SellTypeDto>>> GetAllAsync()
        {
            Result<List<SellTypeDto>> result = new();
            try
            {
                List<SellType> entitesGetted = await _sellTypeRepository.GetAllAsync();

                result.Data = _mapper.Map<List<SellTypeDto>>(entitesGetted);

                result.Message = $"The sell type's get was a success";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critica error while getting all the sell type's";
                return result;
            }
        }
    }
}
