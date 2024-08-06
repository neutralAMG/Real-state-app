using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Dtos.Share;
using FinalProject.Core.Application.Interfaces.Contracts.Share;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Application.Utils.EmailPreBuildRequest;
using FinalProject.Core.Domain.Settings;
using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
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
        private readonly IEmailService _emailService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, HandleRegistration handleRegistration, IEmailService emailService, IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _handleRegistration = handleRegistration;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
        }
        //Todo: Making email sender method for client register
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

            SignInResult result = await _signInManager.PasswordSignInAsync(userAuthenticated, request.Password, false, false).ConfigureAwait(true);


            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = "Incorrect Password please check if you typed it correctly";
                return responce;
            }

            responce.Id = userAuthenticated.Id;

            responce.UserName = userAuthenticated.UserName;

            IList<string> userRoles = await _userManager.GetRolesAsync(userAuthenticated);

            #region Check if the user is active
            if (ApiAuthentication == false)
            {
                if (userRoles.Any(r => r == Roles.Developer.ToString()))
                {
                    responce.HasError = true;
                    responce.ErrorMessage = "Developer's cant use the webapp";
                    return responce;
                }

                if (userRoles.Any( r => r == Roles.Agent.ToString()) && userAuthenticated.EmailConfirmed == false)
                {
                    responce.HasError = true;
                    responce.ErrorMessage = "your account is un activated please wait till and admin activate it";
                    return responce;
                }


                if (userRoles.Any(r => r == Roles.Client.ToString()) && userAuthenticated.EmailConfirmed == false)
                {
                    responce.HasError = true;
                    responce.ErrorMessage = "Your account is un activated check your email to activated";
                    return responce;
                }


                if (userRoles.Any(r => r == Roles.Admin.ToString()) && userAuthenticated.EmailConfirmed == false)
                {
                    responce.HasError = true;
                    responce.ErrorMessage = "Your account is un activated wait till another admin ativate it";
                    return responce;
                }


            }
            #endregion
            responce.Roles = userRoles;
            responce.IsActive = userAuthenticated.EmailConfirmed;

            if (ApiAuthentication)
            {
                if (responce.Roles.Any(r => r == nameof(Roles.Client) || r == nameof(Roles.Agent)))
                {
                    responce.HasError = true;
                    responce.ErrorMessage = "Clients and agents are not authorize to use the api";
                    return responce;
                }
                JwtSecurityToken token = await GenerateJwtTokenAsync(userAuthenticated);
                responce.JwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                var refreshToken = GenerateRefreshToken();
                responce.RefreshToken = refreshToken.Token;
            }
            return responce;
        }
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResponce> RegisterAsync(string role, RegisterRequest request, string origin)
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

            if (responce.HasError) return responce;

            var newUser = await _userManager.FindByNameAsync(request.UserName);
            responce.Id = newUser.Id;

            if (role == Roles.Client.ToString() && !string.IsNullOrEmpty(origin))
            {
                var verificationUri = await SendVerificationEmailUrlAsync(newUser, origin);
                await _emailService.SendEmailAsync(new EmailRequest
                {
                    To = request.Email,
                    Subject = "Activate your user",
                    Body = $"use this link to confirm your email {verificationUri}"
                });
            }



            return responce;
        }
        public async Task<string> ConfirmClientUserEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return string.Empty;
            }
            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var newToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var result = await _userManager.ConfirmEmailAsync(user, newToken);
            if (!result.Succeeded)
            {

            }
            return "";

        }
        private async Task<string> SendVerificationEmailUrlAsync(ApplicationUser user, string origin)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var route = "User/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(Uri.ToString(), "userId", user.Id);
            verificationUrl = QueryHelpers.AddQueryString(verificationUrl, "token", token);

            return verificationUrl;

        }
        public async Task ForgotPasswordAsync(string email, string origin)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {

            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
            var route = "user/ConfirmEmail";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificatinUrl = QueryHelpers.AddQueryString(Uri.ToString(), "token", token);


        }
        public Task ChangePasswordAsync(string password)
        {
            throw new NotImplementedException();
        }
        public async Task<UserOperationResponce> UnLockUserAsync(string id)
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
