

using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Infraestructure.Identity.Entities;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Infraestructure.Identity.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HandleRegistration _handleRegistration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, HandleRegistration handleRegistration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _handleRegistration = handleRegistration;
        }
        public async Task<AuthenticationResponce> AuthenticatAsync(AuthenticationRequest request)
        {
            AuthenticationResponce responce = new();

            ApplicationUser userAuthenticated = await _userManager.FindByNameAsync(request.Username);

            userAuthenticated ??= await _userManager.FindByNameAsync(request.Username);

            if (userAuthenticated == null)
            {
                responce.HasError = true;
                responce.ErrorMessage = "Incorrect user name or email please check if you typed it correctly";
                return responce;
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(userAuthenticated, request.Password, true);

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = "Incorrect Password please check if you typed it correctly";
            }

            responce.Id = userAuthenticated.Id;
            responce.UserName = userAuthenticated.UserName;
            IList<string> Roles = await _userManager.GetRolesAsync(userAuthenticated);
            responce.Roles = Roles;

            return responce;
        }

        public async Task<RegisterResponce> RegisterAsync(RegisterRequest request)
        {
            RegisterResponce responce = new();

            ApplicationUser user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                responce.HasError= true;
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

            responce = await _handleRegistration.HandleRegisterAsync(request.UserType, request);

            return responce;
        }


        public async Task ChangePassword()
        {
            throw new NotImplementedException();
        }

        public async Task ForgotPassword()
        {
            throw new NotImplementedException();
        }
    }
}
