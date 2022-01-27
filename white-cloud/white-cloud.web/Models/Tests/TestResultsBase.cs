namespace white_cloud.web.Models.Tests
{
    public class TestResultsBase
    {
        public TestResultStrategy Strategy { get; set; }
    }

    public enum TestResultStrategy
    {
        None,
        SumIntervals
    }
}
