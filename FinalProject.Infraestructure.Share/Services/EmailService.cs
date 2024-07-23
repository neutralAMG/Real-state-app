
using FinalProject.Core.Application.Dtos.Share;
using FinalProject.Core.Application.Interfaces.Contracts.Share;
using FinalProject.Core.Domain.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FinalProject.Infraestructure.Share.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task SendEmail(EmailRequest request)
        {
            try
            {
                MimeMessage email = new();
                email.Sender = MailboxAddress.Parse($"{_emailSettings.DisplayName} <{_emailSettings.EmailFrom}>");
                email.From.Add(MailboxAddress.Parse(_emailSettings.EmailFrom));
                email.To.Add(MailboxAddress.Parse(request.To));

                email.Subject = request.Subject;

                BodyBuilder bodyBuilder = new()
                {
                    HtmlBody = request.Body,

                };
                email.Body = bodyBuilder.ToMessageBody();

                using (SmtpClient smtpClient = new())
                {
                    smtpClient.Connect(_emailSettings.SmtpHost, _emailSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                    smtpClient.Authenticate(_emailSettings.SmtpUser, _emailSettings.SmtpPass);
                    await smtpClient.SendAsync(email);
                    smtpClient.Disconnect(true);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
