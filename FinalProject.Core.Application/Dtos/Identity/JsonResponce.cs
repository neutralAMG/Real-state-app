

namespace FinalProject.Core.Application.Dtos.Identity
{
    public record JsonResponce
    {
        public bool HasError { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
