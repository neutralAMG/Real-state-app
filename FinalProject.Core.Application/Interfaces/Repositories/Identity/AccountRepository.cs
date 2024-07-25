
using FinalProject.Core.Application.Dtos.Identity.Account;

namespace FinalProject.Core.Application.Interfaces.Repositories.Identity
{
    public interface IAccountRepository
    {
        Task<AuthenticationResponce> AuthenticatAsync(AuthenticationRequest request);
        Task<RegisterResponce> RegisterAsync(RegisterRequest request);
        Task ForgotPassword();
    }
}
