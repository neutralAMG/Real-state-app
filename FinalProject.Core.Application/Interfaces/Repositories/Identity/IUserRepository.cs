

using FinalProject.Core.Application.Dtos.Identity.User;

namespace FinalProject.Core.Application.Interfaces.Repositories.Identity
{
    public interface IUserRepository
    {
        Task<List<GetUserDto>> GetAllBySpecificRoleAsync(string Role);
        Task<GetUserDto> GetByIdAsync(string id);
        Task<UserOperationResponce> UpdateUserAsync(UpdateUserRequest request);
        Task<UserOperationResponce> DeleteUserAsync(string id);
        Task<UserOperationResponce> HandleUserActivationStateAsync(string id, bool Deativate = false);
        Task<GetUserStatisticDto> GetUserStatistics();
        //Make un bolcking request for client users 
    }
}
