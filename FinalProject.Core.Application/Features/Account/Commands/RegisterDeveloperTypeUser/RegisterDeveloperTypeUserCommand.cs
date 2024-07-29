

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Application.Models.User;
using MediatR;

namespace FinalProject.Core.Application.Features.Account.Commands.RegisterDeveloperTypeUser
{
    public class RegisterDeveloperTypeUserCommand : IRequest<Result>
    {

    }

    public class RegisterDeveloperTypeUserCommandHandler : IRequestHandler<RegisterDeveloperTypeUserCommand, Result>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public RegisterDeveloperTypeUserCommandHandler(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(RegisterDeveloperTypeUserCommand request, CancellationToken cancellationToken)
        {
            return await RegisterAsync(request);
        }
        private async Task<Result> RegisterAsync(RegisterDeveloperTypeUserCommand request)
        {
            Result result = new();
            try
            {
                RegisterRequest requestToBeRegister = _mapper.Map<RegisterRequest>(request);

                RegisterResponce responce = await _accountRepository.RegisterAsync("Developer", requestToBeRegister);

                if (responce.HasError)
                {
                    result.ISuccess = false;
                    result.Message = responce.ErrorMessage;
                    return result;
                }

                result.Message = "The account has been successfully created";
                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = "Critical error processing the registration request";
                return result;
            }
        }
    }
}
