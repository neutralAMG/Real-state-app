

using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Models.User;

namespace FinalProject.Core.Application.Interfaces.Contracts.Identity
{
    public interface IAccountService
    {
        Task<Result> AuthenticateWebAppAsync(string username, string password);
            //Remember to dele this onec you make the cqrs method for the api
        Task<Result<AuthenticationResponce>> AuthenticateWebApiAsync(string username, string password);
        Task<Result> RegisterAsync(SaveUserModel saveModel);
        Task<Result> ForgotPassword();
    }

}