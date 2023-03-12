using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Jwt
{
    public class LoginUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}