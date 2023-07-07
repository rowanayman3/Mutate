using System.ComponentModel.DataAnnotations;

namespace JWTRefreshToken.NET6._0.Service
{
    public class ResetPasswordRequest
    {

        [Required]
        public string Token { get; set; } = string.Empty;

        [Required, MinLength(8)]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public string Email { get; set; } 

        
    }
}
