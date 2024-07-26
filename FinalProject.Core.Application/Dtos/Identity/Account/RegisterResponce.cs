

namespace FinalProject.Core.Application.Dtos.Identity.Account
{
    public record RegisterResponce
    {
        public string Id {  get; set; }
        public bool HasError { get; set; }  
        public string? ErrorMessage { get; set; }
        
    }
}
