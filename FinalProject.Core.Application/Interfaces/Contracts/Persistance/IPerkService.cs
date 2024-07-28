
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Models.Perk;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Interfaces.Contracts.Persistance
{
    public interface IPerkService : IBaseCompleteService<PerkModel, SavePerkModel, Perk, int>
    {
    }
}
