

using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Infraestructure.Identity.Seeds
{
    public static class DefaultClientUser
    {
        public static async Task AddDefaultClientUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultAdminUser = new()
            {
                FirstName = "DefaultFirstNamea",
                LastName = "DefaultLastNameb",
                UserName = "clientUser",
                Email = "DefaultClientEmail@gmail.com",
                PhoneNumber = "1234567890",
                ImgProfileUrl = "Img",
                Cedula = " ",
                EmailConfirmed = false,
                PhoneNumberConfirmed = true,

            };;

            if (!userManager.Users.Any(u => u.Id == defaultAdminUser.Id))
            {
                ApplicationUser user = await userManager.FindByEmailAsync(defaultAdminUser.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdminUser, "123Test!");
                    await userManager.AddToRoleAsync(defaultAdminUser, Roles.Client.ToString());
                }
            }
        }
    }
}
