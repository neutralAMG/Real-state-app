using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Utils.SessionHandler;

namespace FinalProject.Presentation.WebApp.Middleware.Validations
{
    public class UserSessionInfoValidations
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly string _userSessionKey;
        private readonly AuthenticationResponce _currentLoginUserInfo;
        public UserSessionInfoValidations(IHttpContextAccessor httpContext, IConfiguration configuration)
        {
            _httpContext = httpContext;
            _userSessionKey = configuration["SessionKeys:UserKey"];
            _currentLoginUserInfo = httpContext.HttpContext.Session.Get<AuthenticationResponce>(_userSessionKey);
        }

        public bool IsUserLogIn()
        {
            return _currentLoginUserInfo != null;
        }

        public bool IsUserActive()
        {
            return _currentLoginUserInfo.IsActive;
        }
        
    }
}
