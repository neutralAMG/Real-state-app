using FinalProject.Presentation.WebApp.Middleware.Validations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalProject.Presentation.WebApp.Middleware.Filters
{
    public class IsThereSubEntitiesNeedItToCreateAProperty : IAsyncActionFilter
    {
        private readonly PropertyValidations _propertyValidations;

        public IsThereSubEntitiesNeedItToCreateAProperty(PropertyValidations propertyValidations)
        {
            _propertyValidations = propertyValidations;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ControllerBase controller;

            if (!await _propertyValidations.IsTherePropertyTypesAvailableAsync())
            {
                controller = (ControllerBase)context.Controller;
                context.Result = controller.RedirectToAction("MantProperty", "Agent");
            }
            else if (!await _propertyValidations.IsTherePerksAvailableAsync())
            {
                controller = (ControllerBase)context.Controller;
                context.Result = controller.RedirectToAction("MantProperty", "Agent");
            }
            else if (!await _propertyValidations.IsThereSellTypesAvailableAsync())
            {
                controller = (ControllerBase)context.Controller;
                context.Result = controller.RedirectToAction("MantProperty", "Agent");
            }
            else
            {
                await next();
            }
        }
    }
}
