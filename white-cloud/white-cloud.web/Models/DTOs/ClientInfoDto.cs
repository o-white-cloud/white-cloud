using white_cloud.entities;

namespace white_cloud.web.Models.DTOs
{
    public class ClientInfoDto
    {
        public int Id { get; set; }
        public DateTime ClientDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
    }
}
