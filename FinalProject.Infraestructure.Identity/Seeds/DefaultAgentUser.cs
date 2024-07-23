﻿
using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Infraestructure.Identity.Seeds
{
    public static class DefaultAgentUser
    {
        public static async Task AddDefaultAgentUser(UserManager<ApplicationUser> userManager)
        {
            ApplicationUser defaultAdminUser = new()
            {
                FirstName = "DefaultFirstName",
                LastName = "DefaultLastName",
                Email = "DefaultAgentEmail@gmail.com",
                PhoneNumber = "1234567890",
                ImgProfileUrl = "Img",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,

            };

            if (userManager.Users.Any(u => u.Id == defaultAdminUser.Id))
            {
                ApplicationUser user = await userManager.FindByEmailAsync(defaultAdminUser.Email);

                if (user == null)
                {
                    await userManager.CreateAsync(defaultAdminUser, "123Test!");
                    await userManager.AddToRoleAsync(defaultAdminUser, Roles.Agent.ToString());
                }
            }
        }
    }
}
