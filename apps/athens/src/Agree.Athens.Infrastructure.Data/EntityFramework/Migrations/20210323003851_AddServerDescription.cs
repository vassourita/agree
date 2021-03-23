using Microsoft.EntityFrameworkCore.Migrations;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Migrations
{
    public partial class AddServerDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Servers",
                type: "character varying(300)",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Servers");
        }
    }
}
