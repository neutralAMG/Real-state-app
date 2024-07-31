

using AutoMapper;
using FinalProject.Core.Application.Core;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Application.Interfaces.Utils;
using FinalProject.Core.Application.Models.User;
using FinalProject.Core.Application.Utils.SessionHandler;
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
        private readonly AuthenticationResponce _currentUserInfo;
        private readonly SessionKeys _sessionKeys;
        private const string basePath = "/Images/ProfilePicture";
        public UserService(IUserRepository userRepository, IFileHandler<string> fileHandler, IMapper mapper, IHttpContextAccessor httpContext, IOptions<SessionKeys> sessionKeys)
        {
            _userRepository = userRepository;
            _fileHandler = fileHandler;
            _mapper = mapper;
            _httpContext = httpContext;
            _sessionKeys = sessionKeys.Value;
            _currentUserInfo = _httpContext.HttpContext.Session.Get<AuthenticationResponce>(_sessionKeys.UserKey);
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

        public async Task<Result> HandleUserActivationStateAsync(string id, bool UserStatus)
        {
            Result result = new();

            string operation = UserStatus == true ? "activativated" : "deactivated";

            try
            {
                if (id == _currentUserInfo.Id)
                {
                    result.ISuccess = false;
                    result.Message = "The current user can't modify itself";
                    return result;
                }

                UserOperationResponce responce = await _userRepository.HandleUserActivationStateAsync(id, UserStatus);

                if (responce.HasError)
                {
                    result.ISuccess = false;
                    result.Message = responce.ErrorMessage;
                    return result;
                }

                result.Message = $"The user was {operation} successfully";

                return result;
            }
            catch
            {
                result.ISuccess = false;
                result.Message = $"Critical error while attempting to {operation} the user ";
                return result;
            }
        }

        public async Task<Result> UpdateUserAsync(SaveUserModel request)
        {
            Result result = new();
            try
            {

                if (request.Id == _currentUserInfo.Id)
                {
                    result.ISuccess = false;
                    result.Message = "The current user can't modify itself";
                }
                request.ImgProfileUrl = _fileHandler.UpdateFile(request.file, basePath, request.ImgProfileUrl, request.Id);

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


                UserOperationResponce responce = await _userRepository.DeleteUserAsync(id);

                if (responce.HasError)
                {
                    result.ISuccess = false;
                    result.Message = responce.ErrorMessage;
                    return result;
                }

                _fileHandler.DeleteFile(basePath, id);
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
