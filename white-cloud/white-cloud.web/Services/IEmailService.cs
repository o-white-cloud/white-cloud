namespace white_cloud.web.Services
{
    public interface IEmailService
    {
        Task SendEmail(string template, object data);
    }
}
