using white_cloud.web.Models.Tests;

namespace white_cloud.web.Services.Tests
{
    public interface ITestService
    {
        Task<TestSubmissionResult> ComputeTestResults(TestModel test, Dictionary<int, string> answers);
    }
}
