using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using white_cloud.entities;
using white_cloud.entities.Tests;

namespace white_cloud.interfaces.Data
{
    public interface IClientRepository
    {
        Task<Client?> GetClient(string userId);
        Task<List<TestRequest>> GetTestRequests(int clientId);
        Task<TestRequest> GetTestRequest(int requestId);
        Task<List<TestSubmission>> GetTestSubmissions(string userId);
        Task<Client?> GetClientTherapist(string userId);
        Task<Client?> GetTherapist(int clientId);
    }
}
