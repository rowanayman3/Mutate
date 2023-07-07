using Microsoft.AspNetCore.Identity;

namespace JWTRefreshToken.NET6._0.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }

        public string? VerifyToken { get; set; } 
    }
}
