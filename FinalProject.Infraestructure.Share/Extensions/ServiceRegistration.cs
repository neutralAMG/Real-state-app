

using FinalProject.Core.Application.Interfaces.Contracts.Share;
using FinalProject.Infraestructure.Share.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject.Infraestructure.Share.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddInfraestructureShareLayer(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<EmailService>(config.GetSection("EmailService"));

            services.AddTransient<IEmailService, EmailService>();

        }
    }
}
