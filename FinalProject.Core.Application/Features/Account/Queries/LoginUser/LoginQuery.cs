

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Domain.Settings;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Core.Application.Features.Account.Queries.LoginUser
{
	/// <summary>
	/// Parameters for the log in of a user's
	/// </summary>
	public class LoginQuery : IRequest<Result<AuthenticationResponce>>
    {
		[SwaggerParameter(Description = "The user email or user name")]
		public string UsernameOrEmail { get; set; }

		[SwaggerParameter(Description = "The user password")]
		public string Password { get; set; }
    }
    public class LoginQueryHandler : IRequestHandler<LoginQuery, Result<AuthenticationResponce>>
    {
        private readonly IAccountRepository _accountRepository;

        public LoginQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;

        }

        public async Task<Result<AuthenticationResponce>> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            return await AuthenticateAsync(request);
        }
        private async Task<Result<AuthenticationResponce>> AuthenticateAsync(LoginQuery request)
        {
            Result<AuthenticationResponce> result = new();
            try
            {
                if (string.IsNullOrEmpty(request.UsernameOrEmail) || string.IsNullOrEmpty(request.Password))
                {
                    result.ISuccess = false;
                    result.Message = "Neather the password nor the username/Email can be empty";
                    return result;
                }

                AuthenticationResponce responce = await _accountRepository.AuthenticateAsync(new AuthenticationRequest
                {  
                    UsernameOrEmail = request.UsernameOrEmail,
                    Password = request.Password,
                  
                }
                ,true);

                if (responce.HasError)
                {
                    result.ISuccess = responce.HasError;
                    result.Message = responce.ErrorMessage;
                    return result;
                }
                result.Data = responce;
                result.Message = "Authentication success";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical error processing your authentication request";
                return result;
            }
        }
    }
}
