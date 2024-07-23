

using FinalProject.Core.Application.Dtos.Share;

namespace FinalProject.Core.Application.Interfaces.Contracts.Share
{
    public interface IEmailService
    {
        Task SendEmail(EmailRequest request);
    }
}
