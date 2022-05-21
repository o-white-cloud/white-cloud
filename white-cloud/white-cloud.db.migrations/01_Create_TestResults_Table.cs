using FluentMigrator;

namespace white_cloud.data.migrations
{
    [MigrationVersion(1, 0, 0, 1, "Initial migration to create necessary tables")]
    public class Create_TestResults_Table : Migration
    {
        public override void Down()
        {
            Delete.Table("TestResults");
        }

        public override void Up()
        {
            Create.Table("test_results")
                .WithDescription("Store the results of psy tests taken by users")
                .WithColumn("id").AsBigSerial()
                .WithColumn("user_email").AsText()
                .WithColumn("answers").AsJsonB()
                .WithColumn("test_id").AsSmallInt()
                .WithColumn("result").AsJsonB()
                .WithColumn("test_timestamp").AsTimestampWithoutTimezone();

        }
    }
}