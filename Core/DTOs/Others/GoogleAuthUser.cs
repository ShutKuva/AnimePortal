using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Others
{
	public class GoogleAuthUser
	{
        [Required]
        public string Name { get; set; } = string.Empty;
     
        [Required]
        public string Email { get; set; } = string.Empty;
    }
}

