

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

        
            return  usersGetted.Select(u =>
                 new GetUserDto()
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    ImgProfileUrl = u.ImgProfileUrl,
                    UserName = u.UserName,
                    Password = u.PasswordHash,
                    Roles =  _userManager.GetRolesAsync(u).Result.ToList()
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

        public Task HandleUserActivationState(string id, bool Deativate = false)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUserAsync(UpdateUserRequest request)
        {
            throw new NotImplementedException();
        }
        public Task DeleteUserAsync(string id)
        {
            throw new NotImplementedException();
        }

    }
}
