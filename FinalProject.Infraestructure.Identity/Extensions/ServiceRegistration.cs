

using FinalProject.Core.Application.Dtos.Identity;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Infraestructure.Identity.Context;
using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace FinalProject.Infraestructure.Identity.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddInfraestrctureIdentityLayerForWebApp(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppIdentityContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultIdentityConnection"), m =>
                m.MigrationsAssembly(typeof(AppIdentityContext).Assembly.FullName));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication();

            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<HandleRegistration>();
            services.AddTransient<IUserRepository, UserRepository>();

        }

        public static void AddInfraestructureIdentityLayerFoeWebApi(IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppIdentityContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultIdentityConnection"), m =>
                m.MigrationsAssembly(typeof(AppIdentityContext).Assembly.FullName));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidAudience = "",
                    ValidIssuer = "",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("")),
                    RoleClaimType = "Roles"


                };

                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = async c =>
                    {
                        c.Response.StatusCode = 500;
                        c.Response.ContentType = "text/plain";
                        await c.Response.WriteAsync(c.Exception.ToString());
                    },
                    OnChallenge = async c =>
                    {
                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JsonResponce()
                        {
                            HasError = false,
                            ErrorMessage = "Your are not Authorized"
                        });

                        await c.Response.WriteAsync(result);
                    },
                    OnForbidden = async c =>
                    {
                        c.Response.StatusCode = 403;
                        c.Response.ContentType = "application/json";
                        var result = JsonConvert.SerializeObject(new JsonResponce()
                        {
                            HasError = true,
                            ErrorMessage = "You are not authorize to access this resource"
                        });
                        await c.Response.WriteAsync(result);
                    }

                };

            });


            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<HandleRegistration>();
            services.AddTransient<IUserRepository, UserRepository>();

        }
    }
}
