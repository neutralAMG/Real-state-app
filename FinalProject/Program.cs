using FinalProject.Core.Application.Extensions;
using FinalProject.Core.Application.Interfaces.Contracts.Identity;
using FinalProject.Core.Application.Interfaces.Contracts.Persistance;
using FinalProject.Core.Application.Services.Identity;
using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Extensions;
using FinalProject.Infraestructure.Identity.Seeds;
using FinalProject.Infraestructure.Persistance.Extensions;
using FinalProject.Infraestructure.Share.Extensions;
using FinalProject.Presentation.WebApp.Extensions;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddInfraestructureIdentityLayerForWebApp(builder.Configuration);
builder.Services.AddInfraestructureShareLayer(builder.Configuration);
builder.Services.AddInfraestructurePersistanceLayer(builder.Configuration);
builder.Services.AddCoreApplicationLayerForWebApp(builder.Configuration);
builder.Services.AddPresentationWebAppLayer();
builder.Services.AddSession();
// Registrar el AccountService
builder.Services.AddScoped<AccountService>();
//builder.Services.AddScoped<AccountService>();

var app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    IServiceProvider services = scope.ServiceProvider;


    try
    {
        UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        RoleManager<IdentityRole> roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
   
        await DefaultRoles.AddDefaulRoles(userManager, roleManager);
        await DefaultClientUser.AddDefaultClientUser(userManager, roleManager);
        await DefaultAdminUser.AddDefaultAddminUser(userManager, roleManager);
        await DefaultAgentUser.AddDefaultAgentUser(userManager, roleManager);
    }
    catch
    {
        throw;
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");



app.Run();
