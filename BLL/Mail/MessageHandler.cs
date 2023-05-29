using System.Text;
using BLL.Abstractions.Interfaces.Mail;
using Core.DB;
using DAL.Abstractions.Interfaces;
using MimeKit;

namespace BLL.Mail
{
    public class MessageHandler : IMessageHandler<MimeMessage, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MimeMessage> CreateMessage(string identifier, IDictionary<string, string> changeDictionary)
        {
            IEnumerable<MailTemplate> mailTemplatesWithCurrentIdentifier =
                await _unitOfWork.MailTemplateRepository.ReadByConditionAsync(mailTemplate =>
                    mailTemplate.Name == identifier);

            MailTemplate mailTemplate;

            try
            {
                mailTemplate = mailTemplatesWithCurrentIdentifier.First();
            }
            catch (InvalidOperationException)
            {
                throw new ArgumentException("There is no mail template with this identifier.");
            }

            StringBuilder mailTemplateString = new StringBuilder();

            mailTemplateString.Append(Encoding.UTF8.GetChars(mailTemplate.Body));

            MimeMessage message = new MimeMessage();

            message.Subject = StringReplacer.ReplaceString(new StringBuilder(mailTemplate.Subject), changeDictionary).ToString();

            BodyBuilder bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = StringReplacer.ReplaceString(mailTemplateString, changeDictionary).ToString();

            return message;
        }
    }
}