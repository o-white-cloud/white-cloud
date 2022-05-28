using Microsoft.AspNetCore.Mvc;

namespace white_cloud.web.Services
{
    public interface IUrlService
    {
        string GetConfirmEmailUrl(string token, string email);
        string GetEmailConfirmedUrl();
        string GetResetPasswordUrl(string token, string email);
        string GetInviteUserEmailUrl(string token, string email, string therapistUserId);
    }
}
