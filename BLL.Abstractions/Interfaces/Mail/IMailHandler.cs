namespace BLL.Abstractions.Interfaces.Mail
{
    public interface IMailHandler<TMessage>
    {
        Task SendMessage(TMessage message);
    }
}