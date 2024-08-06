using FinalProject.Presentation.WebApp.Middleware.Filters;
using FinalProject.Presentation.WebApp.Middleware.Validations;
using FinalProject.Presentation.WebApp.Utils.Interfaces;
using FinalProject.Presentation.WebApp.Utils.WebappSelectListGenerator;
using FinalProject.Presentation.WebApp.Utils.WebbAppCheckBoxGenerator;

namespace FinalProject.Presentation.WebApp.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddPresentationWebAppLayer(this IServiceCollection services)
        {
            services.AddScoped<ISelectListGenerator, SelectListGenerator>();
            services.AddScoped<ICheckBoxGenerator, CheckBoxGenerator>();
            services.AddTransient<PropertyValidations>();
            services.AddTransient<UserSessionInfoValidations>();
            services.AddScoped<IsThereSubEntitiesNeedItToCreateAProperty>();
            services.AddScoped<IsTheUserActive>();
            services.AddScoped<IsUserLogIn>();
            services.AddScoped<IsUserNotLogIn>();
           
        }
    }
}
