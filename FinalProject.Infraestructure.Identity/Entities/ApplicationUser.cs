using Microsoft.AspNetCore.Identity;

namespace FinalProject.Infraestructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImgProfileUrl { get; set; }

    }
}
