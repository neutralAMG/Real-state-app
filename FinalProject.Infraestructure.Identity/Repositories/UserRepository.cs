

using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Infraestructure.Identity.Entities;
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
                 new GetUserDto()
                 {
                     Id = u.Id,
                     FirstName = u.FirstName,
                     LastName = u.LastName,
                     Email = u.Email,
                     ImgProfileUrl = u.ImgProfileUrl,
                     UserName = u.UserName,
                     Password = u.PasswordHash,
                     Roles = _userManager.GetRolesAsync(u).Result.ToList()
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
                Roles = roles.ToList()
            };
        }

        public async Task HandleUserActivationState(string id, bool Deativate = false)
        {
            ApplicationUser userToHandelState = await _userManager.FindByIdAsync(id);

            if (userToHandelState == null)
            {
                return;
            }
            IdentityResult result = new();
            if (!Deativate)
            {
                userToHandelState.EmailConfirmed = false;
                result = await _userManager.UpdateAsync(userToHandelState);
                return;
            }
            string userTokent = await _userManager.GenerateEmailConfirmationTokenAsync(userToHandelState);
            result = await _userManager.ConfirmEmailAsync(userToHandelState, userTokent);
            return;

        }

        public async Task UpdateUserAsync(UpdateUserRequest request)
        {
            ApplicationUser userToBeUpdate = await _userManager.FindByIdAsync(request.Id);

            if (request.Email != userToBeUpdate.Email && await _userManager.Users.AnyAsync(u => u.Email == request.Email)) return;

            if (request.UserName != userToBeUpdate.UserName && await _userManager.Users.AnyAsync(u => u.UserName == request.UserName)) return;

            if (userToBeUpdate == null)
            {
                return;
            }
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

            }

            if (request.Password != userToBeUpdate.PasswordHash)
            {
                string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(userToBeUpdate);
                result = await _userManager.ResetPasswordAsync(userToBeUpdate, resetPasswordToken, request.Password);
            }

            if (!result.Succeeded)
            {

            }

            return;
        }
        public async Task DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

    }
}
