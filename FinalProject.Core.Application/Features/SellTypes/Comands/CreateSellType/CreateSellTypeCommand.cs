
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Core.Application.Features.SellTypes.Comands.CreateSellType
{
	/// <summary>
	/// Parameters for saving a sale type
	/// </summary>
	public class CreateSellTypeCommand : IRequest<Result<SaveSellTypeDto>>
    {
		/// <example>
		/// rent
		/// </example>
		[SwaggerParameter(Description = "The name of the sale type to save")]
		public string Name { get; set; }
		/// <example>
		/// you pay every month
		/// </example>
		[SwaggerParameter(Description = "The description of the sale type to save")]
		public string Description { get; set; }
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
