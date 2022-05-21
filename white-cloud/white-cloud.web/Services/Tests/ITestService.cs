
using white_cloud.entities.Tests;

namespace white_cloud.web.Services.Tests
{
    public interface ITestService
    {
        Task<TestSubmissionResult> ComputeTestResults(string email, TestModel test, Dictionary<int, string> answers);
    }
}
