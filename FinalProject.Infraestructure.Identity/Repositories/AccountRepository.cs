

using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Utils;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Infraestructure.Identity.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HandleRegistration _handleRegistration;
        private readonly CustomAuthSignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, CustomAuthSignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, HandleRegistration handleRegistration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _handleRegistration = handleRegistration;
        }
        public async Task<AuthenticationResponce> AuthenticateAsync(AuthenticationRequest request, bool ApiAuthentication = false)
        {
            AuthenticationResponce responce = new();

            ApplicationUser userAuthenticated = await _userManager.FindByNameAsync(request.Username)
                ?? await _userManager.FindByEmailAsync(request.Username);

            if (userAuthenticated == null)
            {
                responce.HasError = true;
                responce.ErrorMessage = "Incorrect user name or email please check if you typed it correctly";
                return responce;
            }

            SignInResult result = await _signInManager.PasswordSignInAsync(userAuthenticated, request.Password, false, true);

            if (result.IsLockedOut)
            {
                responce.HasError = true;
                responce.ErrorMessage = "This user account i bolcked becaouse of multiple false attemp's";
                responce.IsLockOut = true;
                return responce;
            }

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = "Incorrect Password please check if you typed it correctly";
                return responce;
            }

            responce.Id = userAuthenticated.Id;
            responce.UserName = userAuthenticated.UserName;
            IList<string> Roles = await _userManager.GetRolesAsync(userAuthenticated);
            responce.Roles = Roles;

            return responce;
        }

        public async Task<RegisterResponce> RegisterAsync(string role, RegisterRequest request)
        {
            RegisterResponce responce = new();

            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                responce.HasError = true;
                responce.ErrorMessage = $"Theres already a user with the email: {request.Email}";
                return responce;
            }

            user = await _userManager.FindByNameAsync(request.UserName);

            if (user != null)
            {
                responce.HasError = true;
                responce.ErrorMessage = $"there's already a user with the username: {request.UserName}";
                return responce;
            }

            responce = await _handleRegistration.HandleRegisterAsync(role, request);

            return responce;
        }

        public async Task ForgotPassword()
        {
            throw new NotImplementedException();
        }

        public async Task<UserOperationResponce> UnLockUser(string id)
        {
            UserOperationResponce responce = new();

            ApplicationUser user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                responce.HasError= true;
                responce.ErrorMessage = "User was not found";
                return responce;
            }
            IdentityResult result = new();

            result = await _userManager.SetLockoutEnabledAsync(user, false);

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = result.Errors.First().Description;
                return responce;
            }
            result =  await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = result.Errors.First().Description;
                return responce;
            }
            return responce;
        }
    }
}
