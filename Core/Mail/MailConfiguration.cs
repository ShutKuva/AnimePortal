using MailKit.Security;

namespace Core.Mail
{
    public class MailConfiguration
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public SecureSocketOptions SecureSocketOption { get; set; } 
    }
}