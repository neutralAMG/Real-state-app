

using FinalProject.Core.Application.Dtos.Identity;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Core.Domain.Settings;
using FinalProject.Infraestructure.Identity.Context;
using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Repositories;
using FinalProject.Infraestructure.Identity.Utils;
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
        public static void AddInfraestructureIdentityLayerForWebApp(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppIdentityContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultIdentityConnection"), m =>
                m.MigrationsAssembly(typeof(AppIdentityContext).Assembly.FullName));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //options.User.RequireUniqueEmail = true; 
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequireDigit = true;
                //options.Password.RequiredLength = 8;
                //options.Password.RequireUppercase = true;
            
            }).AddEntityFrameworkStores<AppIdentityContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication();

            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<HandleRegistration>();
            services.AddTransient<IUserRepository, UserRepository>();

            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = "";
                options.LoginPath = "";
            });

        }

        public static void AddInfraestructureIdentityLayerForWebApi(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppIdentityContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultIdentityConnection"), m =>
                m.MigrationsAssembly(typeof(AppIdentityContext).Assembly.FullName));
            });
           
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                //options.User.RequireUniqueEmail = true;
                //options.Password.RequireNonAlphanumeric = true;
                //options.Password.RequireDigit = true;
                //options.Password.RequiredLength = 8;
                //options.Password.RequireUppercase = true;
            }).AddEntityFrameworkStores<AppIdentityContext>()
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
                    ValidAudience = config["JwtSettings:Audience"],
                    ValidIssuer = config["JwtSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"])),
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
            services.AddTransient<SignInManager<ApplicationUser>, CustomAuthSignInManager<ApplicationUser>>();
            services.AddTransient<IUserRepository, UserRepository>();

        }
    }
}
