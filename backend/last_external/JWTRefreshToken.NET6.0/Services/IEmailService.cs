using JWTRefreshToken.NET6._0.Auth;

namespace JWTRefreshToken.NET6._0.Service
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);

    }
}
