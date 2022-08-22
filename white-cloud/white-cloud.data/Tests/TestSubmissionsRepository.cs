using Microsoft.Extensions.Logging;
using white_cloud.data.EF;
using white_cloud.entities.Tests;
using white_cloud.interfaces.Data;

namespace white_cloud.data.Tests
{
    public class TestSubmissionsRepository : ITestSubmissionsRepository
    {
        private readonly ILogger<TestSubmissionsRepository> _logger;
        private readonly WCDbContext _dbContext;

        public TestSubmissionsRepository(ILogger<TestSubmissionsRepository> logger, WCDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task InsertTestSubmission(TestSubmission testSubmission, TestSubmissionShare? submissionShare = null, int? testRequestId = null)
        {
            _dbContext.TestSubmissions.Add(testSubmission);
            if (submissionShare is not null)
            {
                _dbContext.TestSubmissionShares.Add(submissionShare);
            }
            if (testRequestId is not null)
            {
                var request = await _dbContext.TestRequests.FindAsync(testRequestId);
                if (request is not null)
                { 
                    request.TestSubmissionShare = submissionShare; 
                }
            }
            await _dbContext.SaveChangesAsync();
        }
    }
}
