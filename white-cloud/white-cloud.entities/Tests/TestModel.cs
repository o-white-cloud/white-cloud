using System.Text.Json.Serialization;

namespace white_cloud.entities.Tests
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Icon { get; set; } = "test";
        public string Excerpt { get; set; } = "";
        public string Description { get; set; } = "";
        public List<TestQuestion> Questions { get; set; } = new List<TestQuestion>();
        
        public TestResultsBase Results { get; set; } = new TestResultsBase();
    }
}
