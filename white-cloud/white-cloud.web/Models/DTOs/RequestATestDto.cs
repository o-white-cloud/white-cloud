using System.ComponentModel.DataAnnotations;

namespace white_cloud.web.Models.DTOs
{
    public class RequestATestDto
    {
        [Required]
        public int ClientId { get; set; } = 0;
        [Required]
        public int TestId { get; set; } = 0;
        public bool? SendEmail { get; set; }
    }
}
