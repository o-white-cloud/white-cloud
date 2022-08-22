using System.ComponentModel.DataAnnotations.Schema;

namespace white_cloud.entities.Tests
{
    public class TestSubmission
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string UserId { get; set; } = string.Empty;

        [Column(TypeName = "jsonb")]
        public TestSubmissionAnswer[] Answers { get; set; } = new TestSubmissionAnswer[] { };
        public int ResultId { get; set; }
        [Column(TypeName = "jsonb")]
        public object? ResultData { get; set; }
        public DateTime Timestamp { get; set; }

        public User User { get; set; }
        public List<TestSubmissionShare>? TestSubmissionShares { get; set; }
    }

    public class TestSubmissionAnswer
    {
        public int QuestionId { get; set; }
        public int AnswerValue { get; set; }
    }
}
