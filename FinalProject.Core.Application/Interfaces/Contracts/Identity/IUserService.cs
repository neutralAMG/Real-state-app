

using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Models.User;

namespace FinalProject.Core.Application.Interfaces.Contracts.Identity
{
    public interface IUserService
    {
        Task<Result<List<UserModel>>> GetAllBySpecificRoleAsync(string Role);
        Task<Result<UserModel>> GetByIdAsync(string id);
        Task<Result<UserModel>> GetCurrentUser();
        Task<Result> UpdateUserAsync(SaveUserModel request);
        Task<Result> DeleteUserAsync(string id);
        Task<Result> HandleUserActivationStateAsync(string id);

    }
}
