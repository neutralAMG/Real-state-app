

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using MediatR;

namespace FinalProject.Core.Application.Features.Account.Commands.RegisterAdminTypeUser
{
    public class RegisterAdminTypeUserCommand : IRequest<Result>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImgProfileUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Cedula { get; set; }
    }

    public class RegisterAdminTypeUserCommandHandlet : IRequestHandler<RegisterAdminTypeUserCommand, Result>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMapper _mapper;

        public RegisterAdminTypeUserCommandHandlet(IAccountRepository accountRepository, IMapper mapper)
        {
            _accountRepository = accountRepository;
            _mapper = mapper;
        }

        public async Task<Result> Handle(RegisterAdminTypeUserCommand request, CancellationToken cancellationToken)
        {
            return await RegisterAsync(request);
        }
        public async Task<Result> RegisterAsync(RegisterAdminTypeUserCommand request)
        {
            Result result = new();
            try
            {
                RegisterRequest requestToBeRegister = _mapper.Map<RegisterRequest>(request);

                RegisterResponce responce = await _accountRepository.RegisterAsync("Admin", requestToBeRegister);

                if (responce.HasError)
                {
                    result.ISuccess = false;
                    result.Message = responce.ErrorMessage;
                    return result;
                }

                result.Message = "The account has been created succesfully ";
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
