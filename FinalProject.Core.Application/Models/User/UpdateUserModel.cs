﻿
namespace FinalProject.Core.Application.Models.User
{
    public class UpdateUserModel
    {
       public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImgProfileUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Cedula { get; set; }
        public string role { get; set; }

    }
}
