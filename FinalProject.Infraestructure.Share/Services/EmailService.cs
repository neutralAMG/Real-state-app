
using FinalProject.Core.Application.Dtos.Share;
using FinalProject.Core.Application.Interfaces.Contracts.Share;
using FinalProject.Core.Domain.Settings;
using Microsoft.Extensions.Options;

namespace FinalProject.Infraestructure.Share.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public Task SendEmail(EmailRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
