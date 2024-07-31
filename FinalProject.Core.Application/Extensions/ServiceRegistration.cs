using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Interfaces.Utils;
using FinalProject.Core.Application.Services.Identity;
using FinalProject.Core.Application.Services.Persistance;
using FinalProject.Core.Application.Utils.FileHandler;
using FinalProject.Core.Domain.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace FinalProject.Core.Application.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddCoreApplicationLayerForWebApp(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<IFavoriteUserPropertyService, FavoriteUserPropertyService>();
            services.AddTransient<IPerkService, PerkService>();
            services.AddTransient<IPropertyImageService, PropertyImageService>();
            services.AddTransient<IPropertyPerkService, PropertyPerkService>();
            services.AddTransient<IPropertyService, PropertyService>();
            services.AddTransient<IPropertyTypeService, PropertyTypeService>();
            services.AddTransient<ISellTypeService, SellTypeService>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IFileHandler<Guid>, FileHandler<Guid>>(); 
            services.AddTransient<IFileHandler<string>, FileHandler<string>>(); 

            services.Configure<SessionKeys>(config.GetSection("SessionKeys"));
            services.Configure<BasePathsForFileStorage>(config.GetSection("BasePathsForFileStorage"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void AddCoreApplicationLayerForWebApi(this IServiceCollection services, IConfiguration config)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}
