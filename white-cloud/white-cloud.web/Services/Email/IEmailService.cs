namespace white_cloud.web.Services
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string body);
        Task SendEmail(string to, string subject, string template, object data);
    }
}
