using Microsoft.AspNetCore.Mvc;

namespace white_cloud.web.Services
{
    public class UrlService : UrlServiceBase, IUrlService
    {
        public UrlService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        { }

        public string GetConfirmEmailUrl(string token, string email) => BuildUrl("api/user/confirmEmail", new {token, email}) ;

        public string GetEmailConfirmedUrl() => BuildUrl("login/emailConfirmed");

        public string GetInviteUserEmailUrl(string token, string email, string therapistUserId) => BuildUrl("login/invite", new { token, email, therapistUserId });

        public string GetResetPasswordUrl(string token, string email) => BuildUrl("login/resetPassword", new { token, email });
    }
}
