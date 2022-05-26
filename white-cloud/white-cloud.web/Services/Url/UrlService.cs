﻿using Microsoft.AspNetCore.Mvc;

namespace white_cloud.web.Services
{
    public class UrlService : IUrlService
    {
        public string GetConfirmEmailUrl(IUrlHelper urlHelper, string token, string email, string scheme)
        {
            var url = urlHelper.ActionLink("confirmEmail", "api/user", new { token, email }, scheme);
            if(url == null)
            {
                throw new Exception("Generated email confirm url is null");
            }
            return url;
        }

        public string GetEmailConfirmedUrl()
        {
            return "/login/emailConfirmed";
        }

        public string GetInviteUserEmailUrl(IUrlHelper urlHelper, string token, string email, string therapistUserId, string scheme)
        {
            var url = urlHelper.ActionLink("invite", "login", new { token, email, therapistUserId }, scheme);
            if (url == null)
            {
                throw new Exception("Invite user url is null");
            }
            return url;
        }

        public string GetResetPasswordUrl(IUrlHelper urlHelper, string token, string email, string scheme)
        {
            var url = urlHelper.ActionLink("resetPassword", "login", new { token, email }, scheme);
            if (url == null)
            {
                throw new Exception("Generated password reset url is null");
            }
            return url;
        }
    }
}