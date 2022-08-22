using white_cloud.entities;

namespace white_cloud.web.Models.DTOs
{
    public class ClientTherapistInfoDto
    {
        public int Id { get; set; }
        public DateTime TherapistDate { get; set; }
        public int  ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
