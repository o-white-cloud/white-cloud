using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using white_cloud.data.EF;
using white_cloud.entities;
using white_cloud.entities.Tests;
using white_cloud.interfaces.Data;

namespace white_cloud.data.Repositories
{
    internal class ClientRepository : IClientRepository
    {
        private readonly ILogger<ClientRepository> _logger;
        private readonly WCDbContext _dbContext;

        public ClientRepository(ILogger<ClientRepository> logger, WCDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Client?> GetClient(string userId)
        {
            return await _dbContext.Clients.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Client?> GetTherapist(int clientId)
        {
            var client = await _dbContext.Clients.Include(c => c.Therapist).ThenInclude(t => t.User).FirstOrDefaultAsync(c => c.Id == clientId);
            return client;
        }

        public async Task<Client?> GetClientTherapist(string userId)
        {
            var client = await _dbContext.Clients.Include(c => c.Therapist).ThenInclude(t => t.User).FirstOrDefaultAsync(c => c.UserId == userId);
            return client;
        }

        public async Task<List<TestRequest>> GetTestRequests(int clientId)
        {
            return await _dbContext.TestRequests.Where(r => r.ClientId == clientId && r.TestSubmissionShareId == null).ToListAsync();
        }

        public async Task<List<TestSubmission>> GetTestSubmissions(string userId)
        {
            return await _dbContext.TestSubmissions.Include(s => s.TestSubmissionShares).Where(ts => ts.UserId == userId).ToListAsync();
        }

        public async Task<TestRequest?> GetTestRequest(int requestId)
        {
            return await _dbContext.TestRequests.Include(c => c.Therapist).ThenInclude(t => t.User).FirstOrDefaultAsync(r => r.Id == requestId);
        }
    }
}
