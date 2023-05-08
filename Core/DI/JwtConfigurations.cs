namespace Core.DI
{
    public class JwtConfigurations
    {
        public string AccessSecretCode { get; set; }
        public string RefreshSecretCode { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessLifetime { get; set; }
        public int RefreshLifetime { get; set; }
    }
}