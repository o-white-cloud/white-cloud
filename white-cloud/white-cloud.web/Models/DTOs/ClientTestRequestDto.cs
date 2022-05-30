namespace white_cloud.web.Models.DTOs
{
    public class ClientTestRequestDto
    {
        public int Id { get; set; }
        public DateTime RequestDate { get; set; }
        public int TestId { get; set; } = 0;
        public string TestName { get; set; } = string.Empty;
    }
}
