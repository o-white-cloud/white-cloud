using white_cloud.web.Models.Tests;

namespace white_cloud.web.Services.Tests
{
    public class TestService : ITestService
    {
        private readonly IEnumerable<ITestResultComputer> _testResultComputers;

        public TestService(IEnumerable<ITestResultComputer> testResultComputers)
        {
            _testResultComputers = testResultComputers;
        }

        public async Task<TestSubmissionResult> ComputeTestResults(TestModel test, Dictionary<int, string> answers)
        {
            var computer = _testResultComputers.FirstOrDefault(c => c.Strategy == test.Results.Strategy);
            if (computer == null)
            {
                throw new Exception($"No test results computer was found for strategy {test.Results.Strategy}");
            }
            return await computer.GetResult(test, answers);
        }
    }
}
