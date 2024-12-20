﻿
using System.Text.Json.Serialization;

namespace FinalProject.Core.Application.Dtos.Identity.Account
{
    public record  AuthenticationResponce
    {
        public string Id { get; set; }  
        public string UserName { get; set; }
        public string Email { get; set; }   
        public bool IsLockOut { get; set; }
        public bool IsActive { get; set; }
        public bool HasError { get; set; }
        public IList<string> Roles { get; set; }    
        public string? ErrorMessage { get; set; }
        public string JwtToken { get; set; }
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
