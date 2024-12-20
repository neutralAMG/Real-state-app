﻿
using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Infraestructure.Identity.Seeds
{
    public static class DefaultDeveloperUser
    {
        public static async Task AddDefaultDeveloperUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            ApplicationUser defaultDeveloperUser = new()
            {
                FirstName = "DefaultFirstNameh",
                LastName = "DefaultLastNamei",
                Email = "DefaultDeveloperEmail@gmail.com",
                PhoneNumber = "1234567890",
                Cedula = "3243244",
                UserName = "DeveloperUser",
                ImgProfileUrl = "Img",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

            };
            if (!userManager.Users.Any(u => u.Id == defaultDeveloperUser.Id))
            {
                ApplicationUser user = await userManager.FindByEmailAsync(defaultDeveloperUser.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultDeveloperUser, "123Test!");
                    await userManager.AddToRoleAsync(defaultDeveloperUser, Roles.Developer.ToString());

                }

            }
        }
    }
}
