namespace white_cloud.entities.Tests
{
    public class TestQuestion
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public TestQuestionType Type { get; set; }
        public List<TestQuestionAnswer> Answers { get; set; } = new List<TestQuestionAnswer>();
    }

    public enum TestQuestionType
    {
        choice,
        input,
        range
    }
}
