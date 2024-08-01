using FinalProject.Presentation.WebApp.Middleware.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalProject.Presentation.WebApp.Middleware.Filters
{
    public class IsTheUserActive : IAsyncActionFilter
    {
        private readonly UserSessionInfoValidations _userSessionInfoValidations;

        public IsTheUserActive(UserSessionInfoValidations userSessionInfoValidations)
        {
            _userSessionInfoValidations = userSessionInfoValidations;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!_userSessionInfoValidations.IsUserActive())
            {
                var controller = (ControllerBase)context.Controller;
                context.Result = controller.RedirectToAction("");
            }
            else
            {
                await next();
            }
        }
    }
}
