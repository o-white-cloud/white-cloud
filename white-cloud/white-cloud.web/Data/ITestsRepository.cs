using white_cloud.web.Models.Tests;

namespace white_cloud.web.Data
{
    public interface ITestsRepository
    {
        Task<List<TestModel>> GetTests(bool includeResults = false);
        Task<TestModel?> GetTest(int id, bool includeResults = false);
    }
}
