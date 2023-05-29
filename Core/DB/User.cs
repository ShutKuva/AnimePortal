using Core.Enums;

namespace Core.DB
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string? PasswordHash { get; set; } = String.Empty;
        public string RefreshToken { get; set; } = String.Empty;
        public Roles Roles { get; set; }
    }
}