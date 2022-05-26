using Microsoft.AspNetCore.Mvc;

namespace white_cloud.web.Services
{
    public class DevUrlService : IUrlService
    {
        public string GetConfirmEmailUrl(IUrlHelper urlHelper, string token, string email, string scheme)
        {
            var url = urlHelper.ActionLink("confirmEmail", "api/user", new { token, email }, scheme);
            if (url == null)
            {
                throw new Exception("Generated email confirm url is null");
            }
            return url;
        }

        public string GetEmailConfirmedUrl()
        {
            return "http://localhost:3000/login/emailConfirmed";
        }

        public string GetInviteUserEmailUrl(IUrlHelper urlHelper, string token, string email, string therapistUserId, string scheme)
        {
            var url = $"http://localhost:3000/login/invite?token={token}&email={email}&therapistUserId={therapistUserId}";
            return url;
        }

        public string GetResetPasswordUrl(IUrlHelper urlHelper, string token, string email, string scheme)
        {
            var url = urlHelper.ActionLink("resetPassword", "login", new { token, email }, scheme, "localhost:3000");
            if (url == null)
            {
                throw new Exception("Generated password reset url is null");
            }
            return url;
        }
    }
}
