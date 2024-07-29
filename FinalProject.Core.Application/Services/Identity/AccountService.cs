﻿

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Application.Models.User;
using FinalProject.Core.Application.Utils.SessionHandler;
using FinalProject.Core.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FinalProject.Core.Application.Services.Identity
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly SessionKeys _sessionsKeys;


        public AccountService(IAccountRepository accountRepository, IUserService userService,IMapper mapper, IHttpContextAccessor httpContext, IOptions<SessionKeys> sessionsKey)
        {
            _accountRepository = accountRepository;
            _userService = userService;
            _mapper = mapper;
            _httpContext = httpContext;
            _sessionsKeys = sessionsKey.Value;
        }
        public async Task<Result> AuthenticateWebAppAsync(string usernameOrEmail, string password)
        {
            Result result = new();
            try
            {
                if(string.IsNullOrEmpty(usernameOrEmail) || string.IsNullOrEmpty(password))
                {
                    result.ISuccess = false;
                    result.Message = "Neather the password nor the username/Email can be empty";
                    return result;
                }

                AuthenticationResponce responce = await _accountRepository.AuthenticateAsync(new AuthenticationRequest
                {
                    Password = password,
                    UsernameOrEmail = usernameOrEmail
                }
                );

                if (responce.HasError)
                {
                    result.ISuccess = responce.HasError;
                    result.Message = responce.ErrorMessage;
                    return result;
                }

                _httpContext.HttpContext.Session.Set<AuthenticationResponce>(responce, _sessionsKeys.UserKey);

                result.Message = "Authentication success";
                return result;
            }
            catch
            {
                result.ISuccess= false;
                result.Message = $"Critical error processing your authentication request";
                return result;
            }
        }
        public async Task<Result> RegisterAsync(SaveUserModel saveModel)
        {
            Result result = new();
            try
            {
                RegisterRequest requestToBeRegister = _mapper.Map<RegisterRequest>(saveModel);

                RegisterResponce responce = await _accountRepository.RegisterAsync(saveModel.role, requestToBeRegister);

                if (responce.HasError)
                {
                    result.ISuccess = false;
                    result.Message = responce.ErrorMessage;
                    return result;
                }

                //Todo handel file upload and user update;
                Result<UserModel> userGetted  = await _userService.GetByIdAsync(responce.Id);
           

                result.Message = "Your account has been created succesfully now login!!!!";
                return result;
            }
            catch
            {
                result.ISuccess= false;
                result.Message = "Critical error processing the registration request";
                return result;
            }
        }    

        public async Task<Result> ForgotPassword()
        {
            throw new NotImplementedException();
        }
    }
}
