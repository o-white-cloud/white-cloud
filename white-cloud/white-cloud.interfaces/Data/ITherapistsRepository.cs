using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using white_cloud.entities;
using white_cloud.entities.Tests;

namespace white_cloud.interfaces.Data
{
    public interface ITherapistsRepository
    {
        Task<Therapist?> Get(string userId);
        Task<Therapist?> Get(int id);
        Task<Therapist> AddTherapist(Therapist therapist);
        Task CompleteClientInvite(int therapistId, string clientEmail);
        Task<List<ClientInvite>> GetClientInvites(int therapistId);
        Task<ClientInvite?> GetClientInvite(int id);
        Task<ClientInvite> InsertClientInvite(ClientInvite userInvite);
        Task<ClientInvite> UpdateClientInvite(ClientInvite userInvite);
        Task<Client> AddClient(Client client);
        Task<List<Client>> GetClients(int id);
        Task<Client?> GetClient(int clientId);
        Task<Client?> GetClient(string userId);
        Task RemoveClient(Client client);
        Task<bool> IsClient(int therapistId, int clientId);
        Task<TestRequest> AddTestRequest(TestRequest testRequest);
        Task<List<TestRequest>> GetTestRequests(int therapistId, int clientId);
    }
}
