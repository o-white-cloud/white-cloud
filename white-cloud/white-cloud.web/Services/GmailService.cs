using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using MimeKit;
using System.Text;
using white_cloud.web.Models.Settings;

namespace white_cloud.web.Services
{
    public class GmailService : IEmailService
    {
        private readonly ILogger<GmailService> _logger;
        private readonly EmailSettings _emailSettings;
        private readonly GoogleCredential _googleCredential;

        public GmailService(ILogger<GmailService> logger, EmailSettings emailSettings, GoogleCredential googleCredential)
        {
            _logger = logger;
            _emailSettings = emailSettings;
            _googleCredential = googleCredential;
        }

        public async Task SendEmail(string to, string subject, string body)
        {
            try
            {
                var service = new Google.Apis.Gmail.v1.GmailService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = _googleCredential,
                    ApplicationName = "white-cloud-web-api"
                });

                var mimeMessage = CreateEmailMessage(to, subject, body);
                var memStream = new MemoryStream();
                await mimeMessage.WriteToAsync(memStream);
                var request = service.Users.Messages.Send(new Message()
                {
                    Raw = Convert.ToBase64String(memStream.ToArray())
                }, "me");
                var response = await request.ExecuteAsync();
                _logger.LogInformation("Email sent to {to} using Gmail. Message id: {id}", to, response.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not send email using GMail");
            }
        }

        private MimeMessage CreateEmailMessage(string to, string subject, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailSettings.From));
            emailMessage.To.Add(new MailboxAddress(to));
            emailMessage.Cc.Add(new MailboxAddress(_emailSettings.From));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = body };
            return emailMessage;
        }

        public async Task SendEmail(string to, string subject, string template, object data)
        {
            await SendEmail(to, subject, template, data);
        }
    }
}
