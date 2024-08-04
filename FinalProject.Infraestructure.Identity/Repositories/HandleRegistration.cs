using Azure;
using FinalProject.Core.Application.Dtos.Identity.Account;
using FinalProject.Core.Application.Interfaces.Contracts.Share;
using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;

namespace FinalProject.Infraestructure.Identity.Repositories
{
    public class HandleRegistration
    {
        private Dictionary<string, Func<RegisterRequest, Task<RegisterResponce>>> _registerActions;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;

        public HandleRegistration(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _registerActions = new()
            {
                { Roles.Client.ToString(),  RegisterClientAsync },
                { Roles.Agent.ToString(),  RegisterAgentAsync },
                { Roles.Admin.ToString(),  RegisterAdminAsync },
                { Roles.Developer.ToString(),  RegisterDeveloperAsync }
            };
            _userManager = userManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }
        //TODO: Add email sending fiuntionality

        public async Task<RegisterResponce> HandleRegisterAsync(string role, RegisterRequest request)
        {
            return await _registerActions[role].Invoke(request);
        }

        private async Task<RegisterResponce> RegisterClientAsync(RegisterRequest request)
        {
            RegisterResponce responce = new();
  
            ApplicationUser user = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                ImgProfileUrl = request.ImgProfileUrl,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = result.Errors.First().Description;
                return responce;
            }
            await _userManager.AddToRoleAsync(user, Roles.Client.ToString());
  

            return responce;

        }
        private async Task<RegisterResponce> RegisterAgentAsync(RegisterRequest request)
        {
            RegisterResponce responce = new();

            ApplicationUser user = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                ImgProfileUrl = request.ImgProfileUrl,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = result.Errors.First().Description;
                return responce;
            }
            await _userManager.AddToRoleAsync(user, Roles.Agent.ToString());

      

            return responce;
        }
        private async Task<RegisterResponce> RegisterAdminAsync(RegisterRequest request)
        {
            RegisterResponce responce = new();

            ApplicationUser user = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName= request.UserName,
                PhoneNumber = request.PhoneNumber,
                ImgProfileUrl = request.ImgProfileUrl,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Cedula = request.Cedula,
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = result.Errors.First().Description;
                return responce;
            }
            await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());



            return responce;
        }
        private async Task<RegisterResponce> RegisterDeveloperAsync(RegisterRequest request)
        {
            RegisterResponce responce = new();

            ApplicationUser user = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName= request.UserName,
                PhoneNumber = request.PhoneNumber,
                ImgProfileUrl = request.ImgProfileUrl,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                Cedula = request.Cedula,
            };

            IdentityResult result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                responce.HasError = true;
                responce.ErrorMessage = result.Errors.First().Description;
                return responce;
            }
            result = await _userManager.AddToRoleAsync(user, Roles.Developer.ToString());

      

            return responce;
        }

 
    }
}
