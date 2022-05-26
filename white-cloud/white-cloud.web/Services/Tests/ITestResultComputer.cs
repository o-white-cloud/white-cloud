using white_cloud.entities.Tests;
using white_cloud.entities.Tests.Models;

namespace white_cloud.web.Services.Tests
{
    public interface ITestResultComputer
    {
        TestResultStrategy Strategy { get; }
        Task<TestSubmissionResultModel> GetResult(TestModel model, TestSubmissionAnswer[] answers);
    }
}
