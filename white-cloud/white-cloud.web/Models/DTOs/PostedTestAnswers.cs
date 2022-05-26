using white_cloud.entities.Tests;

namespace white_cloud.web.Models.DTOs
{
    public class PostedTestAnswers
    {
        public string Email { get; set; } = "";
        public int TestId { get; set; }
        public TestSubmissionAnswer[] Answers { get; set; } = new TestSubmissionAnswer[] { };
    }
}
