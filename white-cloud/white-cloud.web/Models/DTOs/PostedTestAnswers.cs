namespace white_cloud.web.Models.DTOs
{
    public class PostedTestAnswers
    {
        public string Email { get; set; } = "";
        public int TestId { get; set; }
        public Dictionary<int, string> Answers { get; set; } = new Dictionary<int, string>();
    }
}
