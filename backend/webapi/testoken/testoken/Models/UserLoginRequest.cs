using System.ComponentModel.DataAnnotations;

namespace testoken.Models
{
    public class UserLoginRequest
    {       
        //if you want login with username 
       // public string Username { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

    }
}
