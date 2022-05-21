using white_cloud.entities.Tests;
using white_cloud.interfaces.Data;

namespace white_cloud.web.Services.Tests
{
    public class TestService : ITestService
    {
        private readonly ILogger<TestService> _logger;
        private readonly IEnumerable<ITestResultComputer> _testResultComputers;
        private readonly ITestSubmissionsRepository _testResultsRepository;

        public TestService(ILogger<TestService> logger, IEnumerable<ITestResultComputer> testResultComputers, ITestSubmissionsRepository testResultsRepository)
        {
            _logger = logger;
            _testResultComputers = testResultComputers;
            _testResultsRepository = testResultsRepository;
        }

        public async Task<TestSubmissionResult> ComputeTestResults(string email, TestModel test, Dictionary<int, string> answers)
        {
            _logger.LogInformation("Computing results for test {id} for user {email}", test.Id, email);
            var computer = _testResultComputers.FirstOrDefault(c => c.Strategy == test.Results.Strategy);
            if (computer == null)
            {
                throw new Exception($"No test results computer was found for strategy {test.Results.Strategy}");
            }
            var results = await computer.GetResult(test, answers);

            await _testResultsRepository.InsertTestSubmission(new TestSubmission
            {
                Email = email,
                Answers = answers,
                Result = results,
                TestId = test.Id,
                Timestamp = DateTime.Now
            });
            return results;
        }
    }
}
