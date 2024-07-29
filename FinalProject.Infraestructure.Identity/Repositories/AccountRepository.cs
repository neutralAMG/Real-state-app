using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Domain.Settings;
using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FinalProject.Infraestructure.Identity.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HandleRegistration _handleRegistration;
        private readonly CustomAuthSignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AccountRepository(UserManager<ApplicationUser> userManager, CustomAuthSignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, HandleRegistration handleRegistration, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _handleRegistration = handleRegistration;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<AuthenticationResponce> AuthenticateAsync(AuthenticationRequest request, bool ApiAuthentication = false)
        {
            AuthenticationResponce responce = new();

            ApplicationUser userAuthenticated = await _userManager.FindByNameAsync(request.UsernameOrEmail)
                ?? await _userManager.FindByEmailAsync(request.UsernameOrEmail);

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

            if (ApiAuthentication)
            {
                JwtSecurityToken token = await GenerateJwtTokenAsync(userAuthenticated);
                responce.JwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                var refreshToken = GenerateRefreshToken();
                responce.RefreshToken = refreshToken.Token;
            }
            return responce;
        }
         public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
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
                responce.HasError = true;
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
            result = await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = result.Errors.First().Description;
                return responce;
            }
            return responce;
        }

        private async Task<JwtSecurityToken> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            List<Claim> roleClaims = new();

            foreach (string role in userRoles)
            {
                roleClaims.Add(new Claim("Roles", role));
            }

            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("UId", user.Id),
                new Claim("UserName", user.UserName)

            }.Union(roleClaims).Union(userClaims);

            SymmetricSecurityKey jwtSymmetricSecurityKey = new(Encoding.UTF8.GetBytes(_jwtSettings.Key));

            SigningCredentials signingCredentials = new SigningCredentials(jwtSymmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: Claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials
                );
            return jwtSecurityToken;

        }
      
        private string RandomTokenStringGenerator()
        {
            using RNGCryptoServiceProvider rNGCryptoServiceProvider = new();

            var randomByte = new byte[40];

            rNGCryptoServiceProvider.GetBytes(randomByte);

            return BitConverter.ToString(randomByte).Replace("-", "");
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenStringGenerator(),

                Expires = DateTime.UtcNow.AddDays(7),

                Created = DateTime.UtcNow,
            };
        }
    }
}
