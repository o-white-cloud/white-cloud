﻿namespace white_cloud.web.Models.DTOs.Authentication
{
    public class RegisterClientInviteModel
    {
        public string InviteToken { get; set; }
        public string TherapistUserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
