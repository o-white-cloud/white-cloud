using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace white_cloud.data.Migrations
{
    public partial class ExtraClientInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ocupation",
                table: "Clients",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ocupation",
                table: "Clients");
        }
    }
}
