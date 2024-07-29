
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Dtos.Identity.User;

namespace FinalProject.Core.Application.Interfaces.Repositories.Identity
{
    public interface IAccountRepository
    {
        Task<AuthenticationResponce> AuthenticateAsync(AuthenticationRequest request, bool ApiAuthentication = false);
        Task<RegisterResponce> RegisterAsync(string Roles, RegisterRequest request);
        Task<UserOperationResponce> UnLockUser(string id);
        Task SignOut();
        Task ForgotPassword();
    }
}
