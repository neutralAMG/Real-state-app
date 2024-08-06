
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Core.Application.Features.SellTypes.Queries.GetSellTypeById
{
	/// <summary>
	/// Parameters for getting a sale type
	/// </summary>
	public class GetSellTypeByIdQuery : IRequest<Result<SellTypeDto>>
    {
		[SwaggerParameter(Description = "The id of the sale type to get")]
		public int Id { get; set; }
    }

    public class GetSellTypeByIdQueryHandler : IRequestHandler<GetSellTypeByIdQuery, Result<SellTypeDto>>
    {
        private readonly ISellTypeRepository _sellTypeRepository;
        private readonly IMapper _mapper;

        public GetSellTypeByIdQueryHandler(ISellTypeRepository sellTypeRepository, IMapper mapper)
        {
            _sellTypeRepository = sellTypeRepository;
            _mapper = mapper;
        }

        public async Task<Result<SellTypeDto>> Handle(GetSellTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await BaseCqrsOperations.GetByIdAsync<SellTypeDto, SellType, int>(_sellTypeRepository, _mapper, request.Id, "Sell type");
        }
        private async Task<Result<SellTypeDto>> GetByIdAsync(int id)
        {
            Result<SellTypeDto> result = new();
            try
            {
                SellType entityGetted = await _sellTypeRepository.GetByIdAsync(id);

                if (entityGetted == null)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while getting the sell type by it's Id";
                    return result;
                }

                result.Data = _mapper.Map<SellTypeDto>(entityGetted);
                result.Message = $"The perk  sell type was a success";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critica error while getting the sell type by it's id ";
                return result;
            }
        }
    }
}
