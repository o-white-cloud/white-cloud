using white_cloud.entities.Tests;

namespace white_cloud.web.Services.Tests
{
    public interface ITestResultComputer
    {
        TestResultStrategy Strategy { get; }
        Task<TestSubmissionResult> GetResult(TestModel model, Dictionary<int, string> answers);
    }
}
