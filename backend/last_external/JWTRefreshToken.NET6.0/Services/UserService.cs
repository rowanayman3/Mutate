using System.Security.Claims;

namespace JWTRefreshToken.NET6._0.Service
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string getName()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null) {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }
            return result;

        }

        public string getRole()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            }
            return result;

        }
    }
}
