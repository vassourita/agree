using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agree.Athens.Infrastructure.Data.Migrations
{
    public partial class UpdateRoleJoins : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_role_server_user_ServerUserId",
                table: "role");

            migrationBuilder.DropIndex(
                name: "IX_role_ServerUserId",
                table: "role");

            migrationBuilder.DropColumn(
                name: "ServerUserId",
                table: "role");

            migrationBuilder.CreateTable(
                name: "ServerUserRole",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ServerUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerUserRole", x => new { x.RoleId, x.ServerUserId, x.Id });
                    table.ForeignKey(
                        name: "FK_ServerUserRole_role_ServerUserId",
                        column: x => x.ServerUserId,
                        principalTable: "role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerUserRole_server_user_ServerUserId",
                        column: x => x.ServerUserId,
                        principalTable: "server_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerUserRole_ServerUserId",
                table: "ServerUserRole",
                column: "ServerUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerUserRole");

            migrationBuilder.AddColumn<int>(
                name: "ServerUserId",
                table: "role",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_role_ServerUserId",
                table: "role",
                column: "ServerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_role_server_user_ServerUserId",
                table: "role",
                column: "ServerUserId",
                principalTable: "server_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
