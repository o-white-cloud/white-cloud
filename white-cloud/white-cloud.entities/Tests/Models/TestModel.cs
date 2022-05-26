namespace white_cloud.entities.Tests.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Icon { get; set; } = "test";
        public string Excerpt { get; set; } = "";
        public string Description { get; set; } = "";
        public List<TestQuestionModel> Questions { get; set; } = new List<TestQuestionModel>();
        
        public TestResultsBaseModel Results { get; set; } = new TestResultsBaseModel();
    }
}
