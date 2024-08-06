using FinalProject.Core.Application.Core;
using FinalProject.Infraestructure.Identity.Enums;
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
			if (_userSessionInfoValidations.IsUserLogIn())
			{
				var controller = (ControllerBase)context.Controller;

				if (_userSessionInfoValidations.IsUserFromRoleSpecific(Roles.Client.ToString()))
				{
					context.Result = controller.RedirectToAction("IndexLogeado", "Home");
				}
				if (_userSessionInfoValidations.IsUserFromRoleSpecific(Roles.Agent.ToString()))
				{
					context.Result = controller.RedirectToAction("IndexAgent", "Home");
				}
				if (_userSessionInfoValidations.IsUserFromRoleSpecific(Roles.Admin.ToString()))
				{
					context.Result = controller.RedirectToAction("IndexAdmin", "Home");
				}
			}
			else
			{
				await next();
			}

		}
	}
}
