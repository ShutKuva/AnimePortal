using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Jwt
{
    public class LoginUser
    {
        [Required]
        public string Login { get; set; } = string.Empty;

        [Required] 
        public string Password { get; set; } = string.Empty;
    }
}