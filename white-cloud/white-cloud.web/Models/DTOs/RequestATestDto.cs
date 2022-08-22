using System.ComponentModel.DataAnnotations;

namespace white_cloud.web.Models.DTOs
{
    public class RequestATestDto
    {
        [Required]
        public int ClientId { get; set; } = 0;
        [Required]
        public List<int> TestIds { get; set; } = new List<int>();
    }
}
