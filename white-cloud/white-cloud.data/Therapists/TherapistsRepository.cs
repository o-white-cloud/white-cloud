using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using white_cloud.data.EF;
using white_cloud.entities;
using white_cloud.entities.Tests;
using white_cloud.interfaces.Data;

namespace white_cloud.data.Therapists
{
    public class TherapistsRepository : ITherapistsRepository
    {
        private readonly ILogger<TherapistsRepository> _logger;
        private readonly WCDbContext _dbContext;

        public TherapistsRepository(ILogger<TherapistsRepository> logger, WCDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<Therapist?> Get(int id)
        {
            return await _dbContext.Therapists.FindAsync(id);
        }

        public async Task<Therapist?> Get(string userId)
        {
            return await _dbContext.Therapists.Where(t => t.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<Therapist> AddTherapist(Therapist therapist)
        {
            if (therapist == null)
            {
                throw new ArgumentNullException(nameof(therapist));
            }
            _dbContext.Therapists.Add(therapist);
            await _dbContext.SaveChangesAsync();
            return therapist;
        }

        public async Task CompleteClientInvite(int therapistId, string clientEmail)
        {
            var invite = await _dbContext.ClientInvites.Where(x => x.TherapistId == therapistId && x.Email == clientEmail && x.AcceptedDate == null).FirstOrDefaultAsync();
            if (invite == null)
            {
                _logger.LogWarning("Could not find invite for {email} sent by {therapistUserId}", clientEmail, therapistId);
                return;
            }
            invite.AcceptedDate = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ClientInvite>> GetClientInvites(int therapistId)
        {
            return await _dbContext.ClientInvites.Where(x => x.TherapistId == therapistId && x.AcceptedDate == null).OrderByDescending(x => x.SentDate).ToListAsync();
        }

        public async Task<ClientInvite?> GetClientInvite(int id)
        {
            return await _dbContext.ClientInvites.FindAsync(id);
        }

        public async Task<ClientInvite> InsertClientInvite(ClientInvite userInvite)
        {
            _dbContext.ClientInvites.Add(userInvite);
            await _dbContext.SaveChangesAsync();
            return userInvite;
        }

        public async Task<ClientInvite> UpdateClientInvite(ClientInvite clientInvite)
        {
            _dbContext.ClientInvites.Update(clientInvite);
            await _dbContext.SaveChangesAsync();
            return clientInvite;
        }

        public async Task<Client> AddClient(Client client)
        {
            _dbContext.Clients.Add(client);
            await _dbContext.SaveChangesAsync();
            return client;
        }

        public async Task<List<Client>> GetClients(int therapistId)
        {
            return await _dbContext.Clients.Include(nameof(Client.User)).Where(c => c.TherapistId == therapistId).ToListAsync();
        }

        public async Task<Client?> GetClient(int clientId)
        {
            return await _dbContext.Clients.Include(nameof(Client.User)).FirstOrDefaultAsync(c => c.Id == clientId);
        }

        public async Task<TestRequest> AddTestRequest(TestRequest testRequest)
        {
            _dbContext.TestRequests.Add(testRequest);
            await _dbContext.SaveChangesAsync();
            return testRequest;
        }

        public async Task<bool> IsClient(int therapistId, int clientId)
        {
            return await _dbContext.Clients.AnyAsync(c => c.Id == clientId && c.TherapistId == therapistId);
        }

        public async Task<List<TestRequest>> GetTestRequests(int therapistId, int clientId)
        {
            var requests = await _dbContext.TestRequests.Where(r => r.TherapistId == therapistId && r.ClientId == clientId).ToListAsync();
            return requests;
        }
    }
}
