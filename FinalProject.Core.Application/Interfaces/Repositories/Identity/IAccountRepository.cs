
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Dtos.Identity.User;

namespace FinalProject.Core.Application.Interfaces.Repositories.Identity
{
    public interface IAccountRepository
    {
        Task<AuthenticationResponce> AuthenticateAsync(AuthenticationRequest request, bool ApiAuthentication = false);
        Task<RegisterResponce> RegisterAsync(string Roles, RegisterRequest request, string origin = "");
        Task<UserOperationResponce> UnLockUserAsync(string id);
        Task SignOutAsync();
        Task ForgotPasswordAsync(string email, string origin);
        Task<string> ConfirmClientUserEmailAsync(string userId, string token);

		Task ChangePasswordAsync(string password);
    }
}
