

namespace FinalProject.Core.Application.Dtos.Identity.Account
{
    public record RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string ImgProfileUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Cedula { get; set; }  
        public string UserType { get; set; }
    }
}
