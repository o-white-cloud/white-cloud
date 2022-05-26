using Microsoft.AspNetCore.Mvc;

namespace white_cloud.web.Services
{
    public interface IUrlService
    {
        string GetConfirmEmailUrl(IUrlHelper urlHelper, string token, string email, string scheme);
        string GetEmailConfirmedUrl();
        string GetResetPasswordUrl(IUrlHelper urlHelper, string token, string email, string scheme);
        string GetInviteUserEmailUrl(IUrlHelper urlHelper, string token, string email, string therapistUserId, string scheme);
    }
}
