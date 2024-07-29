

using FinalProject.Infraestructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Infraestructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task AddDefaulRoles(RoleManager<IdentityRole> roleManager)
        {
            //TODO:first  check that roles dosent exist 
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Client.ToString() });
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Admin.ToString() });
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Agent.ToString() });
            await roleManager.CreateAsync(new IdentityRole { Name = Roles.Developer.ToString() });
        }
    }
}
