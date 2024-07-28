

using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Models.SellType;
using FinalProject.Core.Domain.Entities;

namespace FinalProject.Core.Application.Interfaces.Contracts.Persistance
{
    public interface ISellTypeService : IBaseCompleteService<SellTypeModel, SaveSellTypeModel, SellType, int>
    {
    }
}
