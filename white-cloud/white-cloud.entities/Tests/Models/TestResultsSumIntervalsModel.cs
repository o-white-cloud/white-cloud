namespace white_cloud.entities.Tests.Models
{
    public class TestResultsSumIntervalsModel : TestResultsBaseModel
    {
        public int NormalizeValue { get; set; }
        public List<TestResultsInterval> Intervals { get; set; } = new List<TestResultsInterval>();
    }

    public class TestResultsInterval
    {
        public int Id { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public string Name { get; set; } = "";
        public string Details { get; set; } = "";
    }
}
