using MimeKit;

namespace BLL.Abstractions.Interfaces.Mail
{
    public interface IMessageHandler<TMessage, TIdentifier>
    {
        Task<TMessage> CreateMessage(TIdentifier identifier, IDictionary<string, string> changeDictionary);
    }
}