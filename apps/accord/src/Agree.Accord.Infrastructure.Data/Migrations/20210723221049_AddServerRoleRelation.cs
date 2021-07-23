using Microsoft.EntityFrameworkCore.Migrations;

namespace Agree.Accord.Infrastructure.Data.Migrations
{
    public partial class AddServerRoleRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ServerId",
                table: "AspNetRoles",
                column: "ServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_Servers_ServerId",
                table: "AspNetRoles",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_Servers_ServerId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_ServerId",
                table: "AspNetRoles");
        }
    }
}
