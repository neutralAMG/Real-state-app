﻿

using FinalProject.Core.Application.Dtos.Identity.User;
using FinalProject.Core.Application.Interfaces.Repositories.Identity;
using FinalProject.Infraestructure.Identity.Entities;
using FinalProject.Infraestructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infraestructure.Identity.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public UserRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}
		public async Task<List<GetUserDto>> GetAllBySpecificRoleAsync(string Role)
		{
			IList<ApplicationUser> usersGetted = await _userManager.GetUsersInRoleAsync(Role);


			return usersGetted.Select(u =>
			{
				var userRoles = _userManager.GetRolesAsync(u).Result;
				return new GetUserDto()
				{
					Id = u.Id,
					FirstName = u.FirstName,
					LastName = u.LastName,
					PhoneNumber = u.PhoneNumber,
					Email = u.Email,
					ImgProfileUrl = u.ImgProfileUrl,
					UserName = u.UserName,
					Password = u.PasswordHash,
					IsActive = u.EmailConfirmed,
					Cedula = u.Cedula,
					Roles = userRoles.ToList()
				};
			}
			).ToList();
		}

		public async Task<GetUserDto> GetByIdAsync(string id)
		{
			ApplicationUser userGetted = await _userManager.FindByIdAsync(id);

			IList<string> roles = await _userManager.GetRolesAsync(userGetted);

			return new GetUserDto()
			{
				Id = userGetted.Id,
				FirstName = userGetted.FirstName,
				LastName = userGetted.LastName,
				PhoneNumber = userGetted.PhoneNumber,
				Email = userGetted.Email,
				ImgProfileUrl = userGetted.ImgProfileUrl,
				Password = userGetted.PasswordHash,
				UserName = userGetted.UserName,
				Cedula = userGetted.Cedula,
				IsActive = userGetted.EmailConfirmed,
				Roles = roles.ToList()
			};
		}

		public async Task<UserOperationResponce> HandleUserActivationStateAsync(string id)
		{
			UserOperationResponce responce = new()
			{
				Operation = "State modification"
			};

			ApplicationUser userToHandelState = await _userManager.FindByIdAsync(id);

			if (userToHandelState == null)
			{
				responce.HasError = true;
				responce.ErrorMessage = "No user was found";
				return responce;
			}
			IdentityResult result = new();

			if (userToHandelState.EmailConfirmed == true)
			{
				userToHandelState.EmailConfirmed = false;
				result = await _userManager.UpdateAsync(userToHandelState);

				if (!result.Succeeded)
				{
					responce.HasError = true;
					responce.ErrorMessage = result.Errors.First().Description;
					return responce;
				}

				return responce;
			}


			string userTokent = await _userManager.GenerateEmailConfirmationTokenAsync(userToHandelState);
			result = await _userManager.ConfirmEmailAsync(userToHandelState, userTokent);

			if (!result.Succeeded)
			{
				responce.HasError = true;
				responce.ErrorMessage = result.Errors.First().Description;
				return responce;
			}
			return responce;

		}
		public async Task<UserOperationResponce> HandleUserActivationStateAsync(string id, bool Deactivate = false)
		{
			UserOperationResponce responce = new()
			{
				Operation = "State modification"
			};

			ApplicationUser userToHandelState = await _userManager.FindByIdAsync(id);

			if (userToHandelState == null)
			{
				responce.HasError = true;
				responce.ErrorMessage = "No user was found";
				return responce;
			}
			IdentityResult result = new();

			if (Deactivate == false)
			{
				userToHandelState.EmailConfirmed = false;
				result = await _userManager.UpdateAsync(userToHandelState);

				if (!result.Succeeded)
				{
					responce.HasError = true;
					responce.ErrorMessage = result.Errors.First().Description;
					return responce;
				}
			}
			if (Deactivate == true)
			{

				string userTokent = await _userManager.GenerateEmailConfirmationTokenAsync(userToHandelState);
				result = await _userManager.ConfirmEmailAsync(userToHandelState, userTokent);


				if (!result.Succeeded)
				{
					responce.HasError = true;
					responce.ErrorMessage = result.Errors.First().Description;
					return responce;
				}
			}


			return responce;

		}
		public async Task<UserOperationResponce> UpdateUserAsync(UpdateUserRequest request)
		{
			UserOperationResponce responce = new()
			{
				Operation = "Update"
			};

			ApplicationUser userToBeUpdate = await _userManager.FindByIdAsync(request.Id);

			if (userToBeUpdate == null)
			{
				responce.HasError = true;
				responce.ErrorMessage = "The user to be update was not found";
				return responce;
			}

			if (request.Email != userToBeUpdate.Email && await _userManager.Users.AnyAsync(u => u.Email == request.Email))
			{
				responce.HasError = true;
				responce.ErrorMessage = $"There is already a user with the Email: {request.Email}";
				return responce;

			};

			if (request.UserName != userToBeUpdate.UserName && await _userManager.Users.AnyAsync(u => u.UserName == request.UserName))
			{
				responce.HasError = true;
				responce.ErrorMessage = $"There is already a user with the username: {request.UserName}";
				return responce;
			};


			IdentityResult result = new();

			userToBeUpdate.FirstName = request.FirstName ?? userToBeUpdate.FirstName;
			userToBeUpdate.LastName = request.LastName ?? userToBeUpdate.LastName;
			userToBeUpdate.UserName = request.UserName ?? userToBeUpdate.UserName;
			userToBeUpdate.PhoneNumber = request.PhoneNumber ?? userToBeUpdate.PhoneNumber;
			userToBeUpdate.Cedula = request.Cedula ?? userToBeUpdate.Cedula;
			userToBeUpdate.ImgProfileUrl = request.ImgProfileUrl ?? userToBeUpdate.ImgProfileUrl;
			userToBeUpdate.Email = request.Email ?? userToBeUpdate.Email;

			result = await _userManager.UpdateAsync(userToBeUpdate);

			if (!result.Succeeded)
			{
				responce.HasError = true;
				responce.ErrorMessage = result.Errors.First().Description;
				return responce;
			}

			if (request.Password == null) return responce;

			if (request.Password != userToBeUpdate.PasswordHash)
			{
				string resetPasswordToken = await _userManager.GeneratePasswordResetTokenAsync(userToBeUpdate);
				result = await _userManager.ResetPasswordAsync(userToBeUpdate, resetPasswordToken, request.Password);

				if (!result.Succeeded)
				{
					responce.HasError = true;
					responce.ErrorMessage = result.Errors.First().Description;
					return responce;
				}
			}



			return responce;
		}
		public async Task<UserOperationResponce> DeleteUserAsync(string id)
		{
			UserOperationResponce responce = new()
			{
				Operation = "Delete"
			};
			ApplicationUser userToBeDelete = await _userManager.FindByIdAsync(id);

			IdentityResult result = await _userManager.DeleteAsync(userToBeDelete);

			if (!result.Succeeded)
			{
				responce.HasError = true;
				responce.ErrorMessage = result.Errors.First().Description;
				return responce;
			}
			return responce;
		}

		public async Task<GetUserStatisticDto> GetUserStatistics()
		{

			IList<ApplicationUser> allOfTheUsers = await _userManager.GetUsersInRoleAsync(Roles.Agent.ToString());
			GetUserStatisticDto data = new();

			data.AmountOfActiveAgentUsers = allOfTheUsers.Where(u => u.EmailConfirmed == true).Count();

			data.AmountOfInActiveAgentUsers = allOfTheUsers.Count - data.AmountOfActiveAgentUsers;

			allOfTheUsers = await _userManager.GetUsersInRoleAsync(Roles.Client.ToString());

			data.AmountOfActiveClienUsers = allOfTheUsers.Where(u => u.EmailConfirmed == true).Count();

			data.AmountOfInActiveClientUsers = allOfTheUsers.Count - data.AmountOfActiveClienUsers;

			allOfTheUsers = await _userManager.GetUsersInRoleAsync(Roles.Developer.ToString());

			data.AmountOfActiveDeveloperUsers = allOfTheUsers.Where(u => u.EmailConfirmed == true).Count();

			data.AmountOfInActiveDeveloperUsers = allOfTheUsers.Count - data.AmountOfActiveDeveloperUsers;

			return data;
		}
	}
}
