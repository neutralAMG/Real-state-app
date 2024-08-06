

using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Models.User;

namespace FinalProject.Core.Application.Interfaces.Contracts.Identity
{
    public interface IAccountService
    {
        Task<Result> AuthenticateWebAppAsync(string usernameOrEmail, string password);
        Task<Result> RegisterAsync(SaveUserModel saveModel, string origin = "");
        Task<Result> SignOutAsync();
		Task<Result> ConfirmEmail(string email, string token);
		Task<Result> ForgotPassword();
    }

}