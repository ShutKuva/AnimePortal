using BLL.Abstractions.Interfaces.Mail;
using Core.Mail;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace BLL.Mail
{
    public class MailHandler : IMailHandler<MimeMessage>
    {
        private readonly MailConfiguration _mailConfigurations;

        public MailHandler(IOptions<MailConfiguration> mailConfigurations)
        {
            _mailConfigurations = mailConfigurations.Value ?? throw new ArgumentNullException(nameof(mailConfigurations));
        }

        public async Task SendMessage(MimeMessage message)
        {
            using (SmtpClient smtpClient = new SmtpClient())
            {
                await smtpClient.ConnectAsync(_mailConfigurations.Server, _mailConfigurations.Port, _mailConfigurations.SecureSocketOption);

                if (smtpClient.Capabilities.HasFlag(SmtpCapabilities.Authentication))
                {
                    await smtpClient.AuthenticateAsync(_mailConfigurations.Login, _mailConfigurations.Password);
                }

                await smtpClient.SendAsync(message);
            }
        }
    }
}