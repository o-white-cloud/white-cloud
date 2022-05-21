namespace white_cloud.entities.Tests
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
