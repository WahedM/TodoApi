using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTOs
{
    public class RegisterDto
    {
        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(8), MaxLength(100)]
        public string Password { get; set; } = string.Empty;
    }
    public class LoginDto
    {
        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(8), MaxLength(100)]
        public string Password { get; set; } = string.Empty;
    }
    public class AuthResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpireAt { get; set; }
    }
}
