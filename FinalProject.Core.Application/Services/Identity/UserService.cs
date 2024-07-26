

using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Models.User;

namespace FinalProject.Core.Application.Services.Identity
{
    public class UserService : IUserService
    {
        public UserService()
        {
            
        }
        public Task<Result> DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result<List<UserModel>>> GetAllBySpecificRoleAsync(string Role)
        {
            throw new NotImplementedException();
        }

        public Task<Result<UserModel>> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Result> HandleUserActivationState(string id, bool Deativate = false)
        {
            throw new NotImplementedException();
        }

        public Task<Result> UpdateUserAsync(UpdateUserModel request)
        {
            throw new NotImplementedException();
        }
    }
}
