﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
using white_cloud.entities.Tests;
using white_cloud.entities.Tests.Models;
using white_cloud.interfaces.Data;

namespace white_cloud.data.Tests
{
    public class FilesTestsRepository : ITestsRepository
    {
        private readonly ILogger<FilesTestsRepository> _logger;
        private readonly IMemoryCache _cache;
        private readonly string _cacheTestsKey = "tests";
        private readonly string _cacheTestsDictKey = "testsDict";
        public FilesTestsRepository(ILogger<FilesTestsRepository> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        public async Task<Dictionary<int, TestModel>> GetTestsMap()
        {
            if (_cache.TryGetValue(_cacheTestsDictKey, out Dictionary<int, TestModel> cachedTests))
            {
                return cachedTests;
            }
            var tests = await GetTests(false);
            var dict = tests.ToDictionary(t => t.Id, t => t);
            var cacheEntry = _cache.CreateEntry(_cacheTestsDictKey);
            cacheEntry.Value = dict;
            return dict;
        }

        public async Task<List<TestModel>> GetTests(bool includeResults = false)
        {
            if (_cache.TryGetValue(_cacheTestsKey, out List<TestModel> cachedTests))
            {
                return cachedTests;
            }

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
                            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase),
                            new TestResultsJsonConverter(includeResults)
                        },

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
            var cacheEntry = _cache.CreateEntry(_cacheTestsKey);
            cacheEntry.Value = list;
            return list;
        }

        public async Task<TestModel?> GetTest(int id, bool includeResults = false)
        {
            var tests = await GetTests(includeResults);
            return tests.FirstOrDefault(t => t.Id == id);
        }

        public async Task RefreshTests()
        {
            await GetTests();
        }
    }

    class TestResultsJsonConverter : JsonConverter<TestResultsBaseModel>
    {
        private readonly bool _deserializeResults;

        public TestResultsJsonConverter(bool deserializeResults)
        {
            _deserializeResults = deserializeResults;
        }

        public override TestResultsBaseModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            using (var jsonDocument = JsonDocument.ParseValue(ref reader))
            {
                if (!_deserializeResults)
                {
                    return new TestResultsBaseModel();
                }
                if (!jsonDocument.RootElement.TryGetProperty(nameof(TestResultsBaseModel.Strategy).ToLowerInvariant(), out var strategyProperty))
                    return new TestResultsBaseModel();

                var resultStrategy = Enum.Parse<TestResultStrategy>(strategyProperty.GetString() ?? "");
                var resultsObject = new TestResultsBaseModel();
                switch (resultStrategy)
                {
                    case TestResultStrategy.SumIntervals: resultsObject = JsonSerializer.Deserialize<TestResultsSumIntervalsModel>(jsonDocument, options); break;
                }

                return resultsObject ?? new TestResultsBaseModel();
            }
        }

        public override void Write(Utf8JsonWriter writer, TestResultsBaseModel value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, new TestResultsBaseModel(), options);
        }
    }
}
