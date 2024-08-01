using FinalProject.Presentation.WebApp.Middleware.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalProject.Presentation.WebApp.Middleware.Filters
{
    public class IsUserLogIn : IAsyncActionFilter
    {
        private readonly UserSessionInfoValidations _userSessionInfoValidations;

        public IsUserLogIn(UserSessionInfoValidations userSessionInfoValidations)
        {
            _userSessionInfoValidations = userSessionInfoValidations;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!_userSessionInfoValidations.IsUserLogIn())
            {
                var controller = (ControllerBase)context.Controller;

                context.Result = controller.RedirectToAction();
            }
            else
            {
                await next();
            }

        }
    }
}
