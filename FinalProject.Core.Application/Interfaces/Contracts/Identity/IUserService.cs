

using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Models.User;

namespace FinalProject.Core.Application.Interfaces.Contracts.Identity
{
    public interface IUserService
    {
        Task<Result<List<UserModel>>> GetAllBySpecificRoleAsync(string Role);
        Task<Result<UserModel>> GetByIdAsync(string id);
        Task<Result> UpdateUserAsync(UpdateUserModel request);
        Task<Result> DeleteUserAsync(string id);
        Task<Result> HandleUserActivationState(string id, bool Deativate = false);
    }
}
