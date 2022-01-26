using white_cloud.web.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace white_cloud.web.Data
{
    public class FilesTestsRepository : ITestsRepository
    {
        private readonly ILogger<FilesTestsRepository> _logger;

        public FilesTestsRepository(ILogger<FilesTestsRepository> logger)
        {
            _logger = logger;
        }

        public async Task<List<TestModel>> GetTests()
        {
            var list = new List<TestModel>();
            foreach (var file in Directory.EnumerateFiles("test_files"))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(file);
                    var testModel = JsonSerializer.Deserialize<TestModel>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        Converters =
                        {
                            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
                        }
                    });
                    if (testModel != null)
                    {
                        list.Add(testModel);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Could not parse test file {file}", file);
                }
            }
            return list;
        }
    }
}
