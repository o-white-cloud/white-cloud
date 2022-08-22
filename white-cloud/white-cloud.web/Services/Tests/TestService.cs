using white_cloud.entities;
using white_cloud.entities.Tests;
using white_cloud.entities.Tests.Models;
using white_cloud.interfaces.Data;

namespace white_cloud.web.Services.Tests
{
    public class TestService : ITestService
    {
        private readonly ILogger<TestService> _logger;
        private readonly IEnumerable<ITestResultComputer> _testResultComputers;
        private readonly ITestSubmissionsRepository _testSubmissionRepository;
        private readonly IClientRepository _clientRepository;

        public TestService(
            ILogger<TestService> logger, 
            IEnumerable<ITestResultComputer> testResultComputers, 
            ITestSubmissionsRepository testSubmissionRepository,
            IClientRepository clientRepository)
        {
            _logger = logger;
            _testResultComputers = testResultComputers;
            _testSubmissionRepository = testSubmissionRepository;
            _clientRepository = clientRepository;
        }

        public async Task<TestSubmissionResultModel> SubmitTestWithRequest(TestModel test, TestSubmissionAnswer[] answers, string userId, int requestId)
        {
            var client = await _clientRepository.GetClient(userId);
            if(client == null)
            {
                throw new ArgumentException($"Could not find client for user {userId}");
            }

            var result = await ComputeTestResults(test, answers);
            var testSubmission = new TestSubmission
            {
                Answers = answers,
                TestId = test.Id,
                ResultId = result.ResultId,
                ResultData = result.ResultData,
                Timestamp = DateTime.UtcNow,
                UserId = userId,
            };
            var testSubmissionShare = new TestSubmissionShare
            {
                ShareDate = DateTime.UtcNow,
                TestSubmission = testSubmission,
                TherapistId = client.TherapistId,
                UserId = userId,
            };
            await _testSubmissionRepository.InsertTestSubmission(testSubmission, testSubmissionShare, requestId);
            return result;
        }

        private async Task<TestSubmissionResultModel> ComputeTestResults(TestModel test, TestSubmissionAnswer[] answers)
        {
            _logger.LogInformation("Computing results for test {id}", test.Id);
            var computer = _testResultComputers.FirstOrDefault(c => c.Strategy == test.Results.Strategy);
            if (computer == null)
            {
                throw new Exception($"No test results computer was found for strategy {test.Results.Strategy}");
            }
            var results = await computer.GetResult(test, answers);
            return results;
        }
    }
}
