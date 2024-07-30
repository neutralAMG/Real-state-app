

namespace FinalProject.Core.Application.Dtos.EntityDtos
{
    public class UserDto
    {
        public string Id { get; set; }
  
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImgProfileUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Cedula { get; set; }    
        public int AmountOfProperties { get; set; }
        public List<string> Roles { get; set; }
    }
   
}
