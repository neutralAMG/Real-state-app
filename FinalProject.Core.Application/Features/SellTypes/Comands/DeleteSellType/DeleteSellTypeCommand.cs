using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Core.Application.Features.SellTypes.Comands.DeleteSellType
{
	/// <summary>
	/// Parameters for deleting a sale type By id
	/// </summary>
	public class DeleteSellTypeCommand : IRequest<Result>
    {
	
		[SwaggerParameter(Description = "The id of the sale type to deleted")]
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
