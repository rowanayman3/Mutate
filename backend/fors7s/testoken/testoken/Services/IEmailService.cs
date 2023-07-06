using testoken.Models;

namespace testoken.Services
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);

    }
}
