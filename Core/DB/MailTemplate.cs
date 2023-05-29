namespace Core.DB
{
    public class MailTemplate : BaseEntity
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public byte[] Body { get; set; }
    }
}