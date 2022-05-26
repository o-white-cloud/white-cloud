namespace white_cloud.entities.Tests.Models
{
    public class TestQuestionModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public TestQuestionType Type { get; set; }
        public List<TestQuestionAnswerModel> Answers { get; set; } = new List<TestQuestionAnswerModel>();
    }

    public enum TestQuestionType
    {
        choice,
        input,
        range
    }
}
