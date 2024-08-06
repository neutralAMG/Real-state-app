﻿

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Application.Models.User;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace FinalProject.Core.Application.Features.Account.Commands.RegisterDeveloperTypeUser
{
	/// <summary>
	/// Parameters for the creation of Admin user's
	/// </summary>
	public class RegisterDeveloperTypeUserCommand : IRequest<Result>
	{
		/// <example>
		/// Jhon
		/// </example>
		[SwaggerParameter(Description = "The user first name")]
		public string FirstName { get; set; }
		/// <example>
		/// Doe
		/// </example>
		[SwaggerParameter(Description = "The user last name")]
		public string LastName { get; set; }
		/// <example>
		/// UserName
		/// </example>
		[SwaggerParameter(Description = "The user desired user name")]
		public string UserName { get; set; }
		/// <example>
		/// ExampleEmail
		/// </example>
		[SwaggerParameter(Description = "The user's email")]
		public string Email { get; set; }
		/// <example>
		/// 8094458118
		/// </example>
		[SwaggerParameter(Description = "The user phone number")]
		public string PhoneNumber { get; set; }
		[SwaggerParameter(Description = "The user password")]
		public string Password { get; set; }
		/// <example>
		/// 4020992255
		/// </example>
		[SwaggerParameter(Description = "The user cedula")]
		public string Cedula { get; set; }
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
