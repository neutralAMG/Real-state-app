
using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;

namespace FinalProject.Core.Application.Features.Perks.Commands.CreatePerk
{
    public class CreatePerkCommand : IRequest<Result<SavePerkDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreatePerkCommandHandler : IRequestHandler<CreatePerkCommand, Result<SavePerkDto>>
    {
        private readonly IPerkRepository _perkRepository;
        private readonly IMapper _mapper;

        public CreatePerkCommandHandler(IPerkRepository perkRepository, IMapper mapper)
        {
            _perkRepository = perkRepository;
            _mapper = mapper;
        }
        public async Task<Result<SavePerkDto>> Handle(CreatePerkCommand request, CancellationToken cancellationToken)
        {        
            return await SaveAsync(request);
        }
        private async Task<Result<SavePerkDto>> SaveAsync(CreatePerkCommand saveModel)
        {
            Result<SavePerkDto> result = new();
            try
            {
                Perk entityToBeSave = _mapper.Map<Perk>(saveModel);

                Perk entitiSaved = await _perkRepository.SaveAsync(entityToBeSave);

                if (entitiSaved == null)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while attempting to save the perk";
                    return result;
                }

                result.Data = _mapper.Map<SavePerkDto>(entitiSaved);
                result.Message = $"The perk was saved successfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical Error while attempting to save the perk";
                return result;
            }
        }
    }
}
