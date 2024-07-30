

using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infraestructure.Identity.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<List<GetUserDto>> GetAllBySpecificRoleAsync(string Role)
        {
            IList<ApplicationUser> usersGetted = await _userManager.GetUsersInRoleAsync(Role);


            return usersGetted.Select(u =>
            {
                var userRoles = _userManager.GetRolesAsync(u).Result;
                return new GetUserDto()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    ImgProfileUrl = u.ImgProfileUrl,
                    UserName = u.UserName,
                    Password = u.PasswordHash,
                    IsActive = u.EmailConfirmed,
                    Cedula = userRoles.Any(r => r == Roles.Developer.ToString() || r == Roles.Agent.ToString()) ? u.Cedula : null,
                    Roles = userRoles.ToList()
                };
            }    
            ).ToList();
        }

        public async Task<GetUserDto> GetByIdAsync(string id)
        {
            ApplicationUser userGetted = await _userManager.FindByIdAsync(id);

            IList<string> roles = await _userManager.GetRolesAsync(userGetted);

            return new GetUserDto()
            {
                Id = userGetted.Id,
                FirstName = userGetted.FirstName,
                LastName = userGetted.LastName,
                Email = userGetted.Email,
                ImgProfileUrl = userGetted.ImgProfileUrl,
                Password = userGetted.PasswordHash,
                UserName = userGetted.UserName,
                Cedula  = roles.Any(r => r == Roles.Developer.ToString() || r == Roles.Agent.ToString()) ? userGetted.Cedula : null,
                IsActive = userGetted.EmailConfirmed,
                Roles = roles.ToList()
            };
        }

        public async Task<UserOperationResponce> HandleUserActivationStateAsync(string id, bool Deactivate = false)
        {
            UserOperationResponce responce = new()
            {
                Operation = !Deactivate ? "Activation" : "Deactivation"
            };

            ApplicationUser userToHandelState = await _userManager.FindByIdAsync(id);

            if (userToHandelState == null)
            {
                responce.HasError = true;
                responce.ErrorMessage = "No user was found";
                return responce;
            }
            IdentityResult result = new();

            if (!Deactivate)
            {
                userToHandelState.EmailConfirmed = false;
                result = await _userManager.UpdateAsync(userToHandelState);

                if (!result.Succeeded)
                {
                    responce.HasError = true;
                    responce.ErrorMessage = result.Errors.First().Description;
                    return responce;
                }

                return responce;
            }

            string userTokent = await _userManager.GenerateEmailConfirmationTokenAsync(userToHandelState);
            result = await _userManager.ConfirmEmailAsync(userToHandelState, userTokent);

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = result.Errors.First().Description;
                return responce;
            }
            return responce;

        }

        public async Task<UserOperationResponce> UpdateUserAsync(UpdateUserRequest request)
        {
            UserOperationResponce responce = new()
            {
                Operation = "Update"
            };

            ApplicationUser userToBeUpdate = await _userManager.FindByIdAsync(request.Id);

            if (userToBeUpdate == null)
            {
                responce.HasError = true;
                responce.ErrorMessage = "The user to be update was not found";
                return responce;
            }

            if (request.Email != userToBeUpdate.Email && await _userManager.Users.AnyAsync(u => u.Email == request.Email))
            {
                responce.HasError = true;
                responce.ErrorMessage = $"There is already a user with the Email: {request.Email}";
                return responce;

            };

            if (request.UserName != userToBeUpdate.UserName && await _userManager.Users.AnyAsync(u => u.UserName == request.UserName))
            {
                responce.HasError = true;
                responce.ErrorMessage = $"There is already a user with the username: {request.UserName}";
                return responce;
             };


            IdentityResult result = new();

            userToBeUpdate.FirstName = request.FirstName;
            userToBeUpdate.LastName = request.LastName;
            userToBeUpdate.UserName = request.UserName;
            userToBeUpdate.PhoneNumber = request.PhoneNumber;
            userToBeUpdate.Cedula = request.Cedula;
            userToBeUpdate.Email = request.Email;

            result = await _userManager.UpdateAsync(userToBeUpdate);

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = result.Errors.First().Description;
                return responce;
            }

            if (request.Password != userToBeUpdate.PasswordHash)
            {
                string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(userToBeUpdate);
                result = await _userManager.ResetPasswordAsync(userToBeUpdate, resetPasswordToken, request.Password);
            }

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = result.Errors.First().Description;
                return responce;
            }

            return responce;
        }
        public async Task<UserOperationResponce> DeleteUserAsync(string id)
        {
            UserOperationResponce responce = new()
            {
                Operation = "Delete"
            };
            ApplicationUser userToBeDelete = await _userManager.FindByIdAsync(id);

            IdentityResult result = await _userManager.DeleteAsync(userToBeDelete);

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage= result.Errors.First().Description;
                return responce;
            }
            return responce;
        }

    }
}
