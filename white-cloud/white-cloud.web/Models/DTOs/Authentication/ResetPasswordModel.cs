using System.ComponentModel.DataAnnotations;

namespace white_cloud.web.Models.DTOs.Authentication
{
    public class ResetPasswordModel
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string Email { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
