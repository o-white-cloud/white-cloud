using System.Net;
using System.Net.Mail;
using white_cloud.web.Models.Settings;

namespace white_cloud.web.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(EmailSettings settings)
        {
            _settings = settings;
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            var fromAddress = new MailAddress(_settings.From);
            var toAddress = new MailAddress(to);

            using (MailMessage message = new MailMessage(fromAddress, toAddress))
            {
                message.Subject = subject;
                message.Body = @$"<p>{body}</p>";
                message.IsBodyHtml = true;

                using(SmtpClient smtpClient = new SmtpClient())
                {
                    smtpClient.Host = _settings.Server;
                    smtpClient.EnableSsl = true;
                    NetworkCredential cred = new NetworkCredential(_settings.User, _settings.Password);
                    smtpClient.UseDefaultCredentials = true;
                    smtpClient.Credentials = cred;
                    smtpClient.Port = _settings.Port;
                    await smtpClient.SendMailAsync(message);
                }
            }
        }

        public Task SendEmail(string to, string subject, string template, object data)
        {
            throw new NotImplementedException();
        }
    }
}
