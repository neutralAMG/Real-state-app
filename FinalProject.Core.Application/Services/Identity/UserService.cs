﻿

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Core.Application.Interfaces.Utils;
using FinalProject.Core.Application.Models.User;
using FinalProject.Core.Application.Utils.SessionHandler;
using FinalProject.Core.Domain.Entities;
using FinalProject.Core.Domain.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FinalProject.Core.Application.Services.Identity
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IFileHandler<string> _fileHandler;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContext;
		private readonly IPropertyRepository _propertyRepository;
		private readonly AuthenticationResponce _currentUserInfo;
		private readonly SessionKeys _sessionKeys;
		private readonly BasePathsForFileStorage _basePathForFileStorage;
		public UserService(IUserRepository userRepository, IFileHandler<string> fileHandler, IMapper mapper, IHttpContextAccessor httpContext, IOptions<BasePathsForFileStorage> basePaths, IOptions<SessionKeys> sessionKeys, IPropertyRepository propertyRepository)
		{
			_userRepository = userRepository;
			_fileHandler = fileHandler;
			_mapper = mapper;
			_httpContext = httpContext;
			_propertyRepository = propertyRepository;
			_sessionKeys = sessionKeys.Value;
			_currentUserInfo = _httpContext.HttpContext.Session.Get<AuthenticationResponce>(_sessionKeys.UserKey);
			_basePathForFileStorage = basePaths.Value;
		}


		public async Task<Result<List<UserModel>>> GetAllBySpecificRoleAsync(string Role)
		{
			Result<List<UserModel>> result = new();
			try
			{
				if (Role == null)
				{
					result.ISuccess = false;
					result.Message = "Role can't be empty";
					return result;
				}

				List<GetUserDto> usersGetted = await _userRepository.GetAllBySpecificRoleAsync(Role);

				result.Data = _mapper.Map<List<UserModel>>(usersGetted);

				foreach (UserModel user in result.Data)
				{
					List<Property> userProperties = await _propertyRepository.GetAllCurrentAgentUserPropertiesAsync(user.Id);

					user.AmountOfProperties = userProperties.Count;
				}
				result.Message = "The user was getted successfully ";

				return result;

			}
			catch
			{
				result.ISuccess = false;
				result.Message = $"Critical error while getting the user's for the role {Role}";
				return result;
			}
		}

		public async Task<Result<UserModel>> GetByIdAsync(string id)
		{
			Result<UserModel> result = new();
			try
			{
				if (id == null)
				{
					result.ISuccess = false;
					result.Message = "The id cant be empty";
					return result;
				}

				GetUserDto userGetted = await _userRepository.GetByIdAsync(id);

				if (userGetted == null)
				{
					result.ISuccess = false;
					result.Message = "Error getting the user";
					return result;
				}

				result.Data = _mapper.Map<UserModel>(userGetted);
				result.Message = "The user was getted susccessfully";
				return result;
			}
			catch
			{
				result.ISuccess = false;
				result.Message = "Critical error while getting the user";
				return result;
			}
		}
		public async Task<Result<UserModel>> GetCurrentUser()
		{
			Result<UserModel> result = new();
			try
			{
				if (_currentUserInfo == null)
				{
					result.ISuccess = false;
					result.Message = "there is no user log in";
					return result;
				}

				GetUserDto userGetted = await _userRepository.GetByIdAsync(_currentUserInfo.Id);

				if (userGetted == null)
				{
					result.ISuccess = false;
					result.Message = "Error getting the user";
					return result;
				}

				result.Data = _mapper.Map<UserModel>(userGetted);
				result.Message = "The user was getted susccessfully";
				return result;
			}
			catch
			{
				result.ISuccess = false;
				result.Message = "Critical error while getting the user";
				return result;
			}
		}
		public async Task<Result> HandleUserActivationStateAsync(string id)
		{
			Result result = new();


			try
			{
				if (id == _currentUserInfo.Id)
				{
					result.ISuccess = false;
					result.Message = "The current user can't modify itself";
					return result;
				}

				UserOperationResponce responce = await _userRepository.HandleUserActivationStateAsync(id);

				if (responce.HasError)
				{
					result.ISuccess = false;
					result.Message = responce.ErrorMessage;
					return result;
				}

				result.Message = $"The user state was modified successfully";

				return result;
			}
			catch
			{
				result.ISuccess = false;
				result.Message = $"Critical error while attempting to modified the user state";
				return result;
			}
		}

		public async Task<Result> UpdateUserAsync(SaveUserModel request)
		{
			Result result = new();
			try
			{
				if (_currentUserInfo != null)
				{
					if (request.Id == _currentUserInfo.Id)
					{
						result.ISuccess = false;
						result.Message = "The current user can't modify itself";
					}
				}

				if (request.file is not null) request.ImgProfileUrl = await _fileHandler.UpdateFile(request.file, _basePathForFileStorage.UserProfilePictureBasePath, request.ImgProfileUrl, request.Id);

				UpdateUserRequest userRequest = _mapper.Map<UpdateUserRequest>(request);

				UserOperationResponce responce = await _userRepository.UpdateUserAsync(userRequest);

				if (responce.HasError)
				{
					result.ISuccess = false;
					result.Message = responce.ErrorMessage;
					return result;
				}

				result.Message = $"The user was updated successfully";
				return result;
			}
			catch
			{
				result.ISuccess = false;
				result.Message = "Critical error while updating the user";
				return result;
			}
		}
		public async Task<Result> DeleteUserAsync(string id)
		{
			Result result = new();
			try
			{

				if (id == _currentUserInfo.Id)
				{
					result.ISuccess = false;
					result.Message = "The current user can't modify itself";
				}

				var properties = await _propertyRepository.GetAllCurrentAgentUserPropertiesAsync(id);

				UserOperationResponce responce = await _userRepository.DeleteUserAsync(id);

				if (responce.HasError)
				{
					result.ISuccess = false;
					result.Message = responce.ErrorMessage;
					return result;
				}

				_fileHandler.DeleteFile(_basePathForFileStorage.UserProfilePictureBasePath, id);
				
					foreach (var property in properties)
					{
						await _propertyRepository.DeleteAsync(property.Id);
					}
				


				result.Message = "The user deletion was a success";
				return result;
			}
			catch
			{
				result.ISuccess = false;
				result.Message = "Critical error while deleting the user";
				return result;
			}
		}


	}

}
