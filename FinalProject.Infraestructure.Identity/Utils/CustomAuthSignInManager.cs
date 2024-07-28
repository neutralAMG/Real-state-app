
using FinalProject.Infraestructure.Identity.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace FinalProject.Infraestructure.Identity.Utils
{
    public class CustomAuthSignInManager<User> : SignInManager<User> where User : class
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<SignInManager<User>> _logger;

        public CustomAuthSignInManager(UserManager<User> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<User> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _userManager = userManager;
            _logger = logger;
        }

        protected override async Task<bool> IsLockedOut(User user)
        {
            try
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                bool IsUserLockOut = await base.IsLockedOut(user);
                if (IsUserLockOut)
                {
                    if (userRoles.Contains(Roles.Admin.ToString()) || userRoles.Contains(Roles.Developer.ToString()))
                    {
                        _logger.LogInformation($"This user ({user}) lockOut state change because admin and developers cant be lockOut fom the app");
                        await _userManager.SetLockoutEnabledAsync(user, false);
                        await _userManager.SetLockoutEndDateAsync(user, DateTime.UtcNow);
                        return false;
                    }
                }
                return IsUserLockOut;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Critical error checking in the user for lockout state" );
                throw;
            }
        }

        //Ensures the developer and admin users cant be lockout
        public override async Task<SignInResult> PasswordSignInAsync(User user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            try
            { 
                if (user == null)
                {
                    _logger.LogInformation($"This user ({user}) Was not found");

                    return SignInResult.Failed;
                }
                var userRoles = await _userManager.GetRolesAsync(user);

                if (userRoles is not null && (userRoles.Contains(Roles.Admin.ToString()) || userRoles.Contains(Roles.Developer.ToString())))
                {
                    await base.ResetLockout(user);
                    return await base.PasswordSignInAsync(user, password, isPersistent, false);
                }
                
                return await base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
            }
            catch(Exception ex) 
            {
                _logger.LogCritical($"Critical error signing in the user ({user}) ", ex);
                return SignInResult.Failed;

            }

        }
    }
}
