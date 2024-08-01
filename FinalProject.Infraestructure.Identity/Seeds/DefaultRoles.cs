

using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Infraestructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task AddDefaulRoles(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //TODO:first  check that roles dosent exist 
            await roleManager.CreateAsync(new IdentityRole( Roles.Client.ToString()));
            await roleManager.CreateAsync(new IdentityRole( Roles.Admin.ToString() ));
            await roleManager.CreateAsync(new IdentityRole ( Roles.Agent.ToString() ));
            await roleManager.CreateAsync(new IdentityRole ( Roles.Developer.ToString() ));
        }
    }
}
