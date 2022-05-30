
using white_cloud.entities.Tests;
using white_cloud.entities.Tests.Models;

namespace white_cloud.interfaces.Data
{
    public interface ITestsRepository
    {
        Task RefreshTests();
        Task<List<TestModel>> GetTests(bool includeResults = false);
        Task<TestModel?> GetTest(int id, bool includeResults = false);
    }
}
