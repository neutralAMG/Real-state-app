

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.EntityDtos;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Domain.Entities;
using MediatR;
using System.Security.Cryptography;

namespace FinalProject.Core.Application.Features.Perks.Commands.UpdatePerk
{
    public class UpdatePerkCommand : IRequest<Result<UpdatePerkDto>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class UpdatePerkCommandHandler : IRequestHandler<UpdatePerkCommand, Result<UpdatePerkDto>>
    {
        private readonly IPerkRepository _perkRepository;
        private readonly IMapper _mapper;

        public UpdatePerkCommandHandler(IPerkRepository perkRepository, IMapper mapper)
        {
            _perkRepository = perkRepository;
            _mapper = mapper;
        }

        public async Task<Result<UpdatePerkDto>> Handle(UpdatePerkCommand request, CancellationToken cancellationToken)
        {
            return await UpdateAsync(request);
        }
        private async Task<Result<UpdatePerkDto>> UpdateAsync(UpdatePerkCommand updateModel)
        {
            Result<UpdatePerkDto> result = new();
            try
            {
                Perk entityToBeUpdate = _mapper.Map<Perk>(updateModel);

                Perk entityUpdated = await _perkRepository.UpdateAsync(entityToBeUpdate);

                if (entityUpdated == null)
                {
                    result.ISuccess = false;
                    result.Message = $"Error while attempting to updated the perk";
                    return result;
                }

                result.Data = _mapper.Map<UpdatePerkDto>(entityUpdated);

                result.Message = $"The perk updated successfully";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical error while attempting to update the perk";
                return result;
            }
        }
    }
}
