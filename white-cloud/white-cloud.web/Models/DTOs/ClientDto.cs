using white_cloud.entities;

namespace white_cloud.web.Models.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime ClientDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int Age { get; internal set; }
        public Gender Gender { get; internal set; }
        public string Ocupation { get; internal set; }
    }
}
