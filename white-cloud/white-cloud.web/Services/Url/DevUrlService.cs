using Microsoft.AspNetCore.Mvc;

namespace white_cloud.web.Services
{
    public class DevUrlService : UrlServiceBase, IUrlService
    {
        public DevUrlService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        { }

        public string GetConfirmEmailUrl(string token, string email) => BuildUrl("api/user/confirmEmail", new { token, email });

        public string GetEmailConfirmedUrl()
        {
            return "http://localhost:3000/login/emailConfirmed";
        }

        public string GetInviteUserEmailUrl(string token, string email, string therapistUserId)
        {
            var url = $"http://localhost:3000/login/invite?token={token}&email={email}&therapistUserId={therapistUserId}";
            return url;
        }

        public string GetResetPasswordUrl(string token, string email)
        {
            var url = $"http://localhost:3000/login/resetPassword?token={token}&email={email}";
            return url;
        }
    }
}
