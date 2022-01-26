using System.Text.Json.Serialization;

namespace white_cloud.web.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Icon { get; set; } = "test";
        public string Excerpt { get; set; } = "";
        public string Description { get; set; } = "";
        public List<TestQuestion> Questions { get; set; } = new List<TestQuestion>();
        [JsonIgnore]
        public object Results { get; set; } = new object();
    }

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

    public class TestQuestionAnswer
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public object Value { get; set; } = 1;
    }
}
