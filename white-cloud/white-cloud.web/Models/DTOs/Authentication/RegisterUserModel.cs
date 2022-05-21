namespace white_cloud.web.Models.DTOs.Authentication
{
    public class RegisterUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? CopsiNumber { get; set; }
        public AccountType AccountType { get; set; }
    }

    public enum AccountType
    {
        Personal = 1,
        Therapist = 2
    }
}
