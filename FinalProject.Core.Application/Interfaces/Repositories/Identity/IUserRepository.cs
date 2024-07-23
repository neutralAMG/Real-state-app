

using FinalProject.Core.Application.Dtos.Identity.User;

namespace FinalProject.Core.Application.Interfaces.Repositories.Identity
{
    public interface IUserRepository
    {
        Task<List<GetUserDto>> GetAllBySpecificRoleAsync(string Role);
        Task<GetUserDto> GetByIdAsync(string id);
        Task UpdateUserAsync(UpdateUserRequest request);
        Task DeleteUserAsync(string id);
        Task HandleUserActivationState(string id, bool Deativate = false);
    }
}
