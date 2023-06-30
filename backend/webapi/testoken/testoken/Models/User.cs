using System.Reflection.PortableExecutable;

namespace testoken.Models
{
    public class User
    {

        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];

        public string? VerificationToken { get; set; }

        public DateTime? VerifyAt { get; set; } = DateTime.Now.AddMinutes(0);

        public string? PasswordResetToken { get; set; }

        public DateTime? PasswordResetExpire { get; set; } = DateTime.Now.AddMinutes(0);

        public string NewToken { get; set; } = string.Empty;

        public DateTime TokenStart { get; set; }

        public DateTime TokenEnd { get; set; }


    }
}
