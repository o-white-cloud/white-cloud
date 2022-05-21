using System.ComponentModel.DataAnnotations;

namespace white_cloud.web.Models.DTOs.Authentication
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
