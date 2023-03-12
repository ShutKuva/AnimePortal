using Core.Enums;

namespace Core.DB
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public Roles Roles { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpires { get; set; }
    }
}