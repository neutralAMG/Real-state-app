
using FinalProject.Core.Application.Dtos.Identity.Account;

namespace FinalProject.Core.Application.Interfaces.Repositories.Identity
{
    public interface IAccountRepository
    {
        Task<AuthenticationResponce> AuthenticateAsync(AuthenticationRequest request, bool ApiAuthentication = false);
        Task<RegisterResponce> RegisterAsync(RegisterRequest request);
        Task ForgotPassword();
    }
}
