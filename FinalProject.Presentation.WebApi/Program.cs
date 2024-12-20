using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Extensions;
using FinalProject.Infraestructure.Identity.Seeds;
using FinalProject.Infraestructure.Persistance.Extensions;
using FinalProject.Core.Application.Extensions;
using FinalProject.Infraestructure.Share.Extensions;
using Microsoft.AspNetCore.Identity;
using FinalProject.Presentation.WebApi.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new ProducesAttribute("application/json"));
}).ConfigureApiBehaviorOptions(options =>
{
    options.SuppressInferBindingSourcesForParameters = true;
    options.SuppressMapClientErrors = true;
});
builder.Services.AddInfraestructureIdentityLayerForWebApi(builder.Configuration);
builder.Services.AddInfraestructurePersistanceLayer(builder.Configuration);
builder.Services.AddInfraestructureShareLayer(builder.Configuration);
builder.Services.AddCoreApplicationLayerForWebApi(builder.Configuration);
builder.Services.AddSession();
builder.Services.AddHealthChecks();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSwaggerExtension();
builder.Services.AddApiVersioningExtension();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;
    try
    {
        UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

        RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.AddDefaulRoles(userManager, roleManager);
        await DefaultAdminUser.AddDefaultAddminUser(userManager, roleManager);
        await DefaultDeveloperUser.AddDefaultDeveloperUser(userManager, roleManager);

    }
    catch
    {
        throw;
    }
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExtension();
}

app.UseHttpsRedirection();
app.UseHealthChecks("/Health");
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
