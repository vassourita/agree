using Microsoft.EntityFrameworkCore.Migrations;

namespace Agree.Allow.Data.Migrations
{
    public partial class AddDisplayNameAndTagIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Tag_DisplayName",
                table: "AspNetUsers",
                columns: new[] { "Tag", "DisplayName" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Tag_DisplayName",
                table: "AspNetUsers");
        }
    }
}
