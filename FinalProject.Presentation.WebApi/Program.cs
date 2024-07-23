using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Extensions;
using FinalProject.Infraestructure.Identity.Seeds;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddInfraestructureIdentityLayerForWebApi(builder.Configuration);
builder.Services.AddSession();
builder.Services.AddHealthChecks();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHealthChecks("/Health");
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;
    try
    {
        UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.AddDefaulRoles(roleManager);
        await DefaultClientUser.AddDefaultClientUser(userManager);
        await DefaultAdminUser.AddDefaultAddminUser(userManager);
        await DefaultAgentUser.AddDefaultAgentUser(userManager);

    }
    catch
    {
        throw;
    }
}
app.Run();
