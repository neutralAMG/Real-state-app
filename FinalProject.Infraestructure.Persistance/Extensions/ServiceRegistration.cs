

using FinalProject.Core.Application.Interfaces.Repositories.Persistance;
using FinalProject.Infraestructure.Persistance.Context;
using FinalProject.Infraestructure.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject.Infraestructure.Persistance.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddInfraestructurePersistanceLayer(this IServiceCollection services, IConfiguration config)
        {
            
            services.AddDbContext<FinalProjectContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"),m =>
                m.MigrationsAssembly(typeof(FinalProjectContext).Assembly.FullName));
            });

            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddTransient<IPropertyPerkRepository, PropertyPerkRepository>();
            services.AddTransient<IPropertyImageRepository, PropertyImageRepository>();
            services.AddTransient<IPropertyTypeRepository, PropertyTypeRepository>();
            services.AddTransient<IPerkRepository, PerkRepository>();
            services.AddTransient<ISellTypeRepository, SellTypeRepository>();
            services.AddTransient<IFavoriteUserPropertyRepository, FavoriteUserPropertyRepository>();


        }
    }
}
