

namespace FinalProject.Core.Application.Dtos.Identity.Account
{
    public record  AuthenticationRequest
    {
        public string Username { get; set; }    
        public string Password { get; set; }
    }
}
