

namespace FinalProject.Core.Application.Dtos.Identity.User
{
    public class UserOperationResponce
    {
        public UserOperationResponce()
        {
            HasError = false;
        }
        public string Operation {  get; set; }
        public bool HasError { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
