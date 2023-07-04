using external_end.Models;
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

        public DateTime? VerifyAt { get; set; } 

        public string? PasswordResetToken { get; set; }

        public DateTime? PasswordResetExpire { get; set; }

        

        public string NewToken { get; set; } = string.Empty;

        public DateTime TokenStart { get; set; }

        public DateTime TokenEnd { get; set; }

        public ICollection<InvalidateToken> InvalidTokens { get; set; } = new List<InvalidateToken>();

    }
}
