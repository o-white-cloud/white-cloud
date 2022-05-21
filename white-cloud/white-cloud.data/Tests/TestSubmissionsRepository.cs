using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using white_cloud.entities.Tests;
using white_cloud.interfaces.Data;

namespace white_cloud.data.Tests
{
    internal class TestSubmissionsRepository : ITestSubmissionsRepository
    {
        private readonly ILogger<TestSubmissionsRepository> _logger;

        public TestSubmissionsRepository(ILogger<TestSubmissionsRepository> logger)
        {
            _logger = logger;
        }

        public async Task InsertTestSubmission(TestSubmission testSubmission)
        {
            //using (var con = _connectionFactory.GetDbConnection() as Npgsql.NpgsqlConnection)
            //{
            //    if(con == null)
            //    {
            //        throw new Exception("Could not get connection to the database");
            //    }
                
            //    await con.OpenAsync();

            //    var query = @"INSERT INTO test_results (user_email, answers, test_id, result, test_timestamp) VALUES(@email, @answers, @testId, @results, @timestamp)";

            //    using var cmd = new NpgsqlCommand(query, con)
            //    {
            //        Parameters =
            //        {
            //            new NpgsqlParameter("email", NpgsqlTypes.NpgsqlDbType.Text) { Value = testSubmission.Email },
            //            new NpgsqlParameter("answers", NpgsqlTypes.NpgsqlDbType.Jsonb) { Value = testSubmission.Answers.Select(a => new { qId = a.Key, value = a.Value}).ToArray()},
            //            new NpgsqlParameter("testId", NpgsqlTypes.NpgsqlDbType.Smallint) { Value = testSubmission.TestId },
            //            new NpgsqlParameter("results", NpgsqlTypes.NpgsqlDbType.Jsonb) { Value = testSubmission.Result},
            //            new NpgsqlParameter("timestamp", NpgsqlTypes.NpgsqlDbType.Timestamp) { Value = testSubmission.Timestamp}
            //        }
            //    };
            //    await cmd.ExecuteNonQueryAsync();
            //}
        }
    }
}
