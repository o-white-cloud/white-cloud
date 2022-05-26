
using white_cloud.entities.Tests;
using white_cloud.entities.Tests.Models;

namespace white_cloud.web.Services.Tests
{
    public interface ITestService
    {
        Task<TestSubmissionResultModel> ComputeTestResults(string email, TestModel test, TestSubmissionAnswer[] answers);
    }
}
