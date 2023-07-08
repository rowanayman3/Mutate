using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using JWTRefreshToken.NET6._0.Auth;
using MailKit.Net.Smtp;

namespace JWTRefreshToken.NET6._0.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("emailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new SmtpClient();
            smtp.Connect(_config.GetSection("EmailHost").Value, 465 , SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_config.GetSection("emailUsername").Value, _config.GetSection("emailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
