using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using MediatR;


namespace FinalProject.Core.Application.Features.Perks.Commands.DeletePerk
{
    public class DeletePerkCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
    public class DeletePerkCommandHandler : IRequestHandler<DeletePerkCommand, Result>
    {
        private readonly IPerkRepository _perkRepository;
        private readonly IMapper _mapper;

        public DeletePerkCommandHandler(IPerkRepository perkRepository, IMapper mapper)
        {
            _perkRepository = perkRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(DeletePerkCommand request, CancellationToken cancellationToken)
        {
            return await DeleteAsync(request.Id);
        }
        private async Task<Result> DeleteAsync(int id)
        {
            Result result = new();
            try
            {
                bool isDeleteOpreationSuccses = await _perkRepository.DeleteAsync(id);
                if (!isDeleteOpreationSuccses)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while attempting to delete the perk";
                    return result;
                }

                result.Message = $"The perk was deleted successfully";
                return result;

            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical error while attemting to delete the perk";
                return result;
            }
        }
    }
}
