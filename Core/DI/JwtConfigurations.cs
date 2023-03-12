namespace Core.DI
{
    public class JwtConfigurations
    {
        public string SecretCode { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Lifetime { get; set; }
        public int RefreshLifetime { get; set; }
    }
}