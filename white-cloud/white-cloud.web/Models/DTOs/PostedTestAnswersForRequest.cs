using white_cloud.entities.Tests;

namespace white_cloud.web.Models.DTOs
{
    public class PostedTestAnswersForRequest
    {
        public int TestId { get; set; }
        public int RequestId { get; set; }
        public TestSubmissionAnswer[] Answers { get; set; } = new TestSubmissionAnswer[] { };
    }
}
