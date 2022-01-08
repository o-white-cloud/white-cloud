using white_cloud.web.Models;

namespace white_cloud.web.Data
{
    public interface ITestsRepository
    {
        public Task<List<TestModel>> GetTests();
    }
}
