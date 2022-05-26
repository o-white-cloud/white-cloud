namespace white_cloud.entities.Tests.Models
{
    public class TestResultsBaseModel
    {
        public TestResultStrategy Strategy { get; set; }
    }

    public enum TestResultStrategy
    {
        None,
        SumIntervals
    }
}
