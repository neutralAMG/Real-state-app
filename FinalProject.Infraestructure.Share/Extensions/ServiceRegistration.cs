

using FinalProject.Core.Application.Interfaces.Contracts.Share;
using FinalProject.Core.Domain.Settings;
using FinalProject.Infraestructure.Share.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject.Infraestructure.Share.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddInfraestructureShareLayer(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<EmailSettings>(config.GetSection("EmailSettings"));

            services.AddTransient<IEmailService, EmailService>();

        }
    }
}
