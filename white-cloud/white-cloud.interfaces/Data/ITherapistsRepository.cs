using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using white_cloud.entities;

namespace white_cloud.interfaces.Data
{
    public interface ITherapistsRepository
    {
        Task<Therapist?> Get(string userId);
        Task<Therapist?> Get(int id);
        Task<Therapist> AddTherapist(Therapist therapist);
        Task CompleteClientInvite(int therapistId, string clientEmail);
        Task<List<ClientInvite>> GetClientInvites(int therapistId);
        Task<ClientInvite> InsertClientInvite(ClientInvite userInvite);
        Task<ClientInvite> UpdateClientInvite(ClientInvite userInvite);
        Task<Client> AddClient(int id, string userId);
        Task<List<Client>> GetClients(int id);
    }
}
