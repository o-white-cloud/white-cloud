
using white_cloud.entities.Tests;

namespace white_cloud.interfaces.Data
{
    public interface ITestsRepository
    {
        Task<List<TestModel>> GetTests(bool includeResults = false);
        Task<TestModel?> GetTest(int id, bool includeResults = false);
    }
}
