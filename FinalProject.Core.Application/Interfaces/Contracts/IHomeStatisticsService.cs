using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Models;

namespace FinalProject.Core.Application.Interfaces.Contracts
{
    public interface IHomeStatisticsService
    {
        Task<Result<HomeViewStatisticsModel>> GetAdminStatisticsAsync();
    }
}
