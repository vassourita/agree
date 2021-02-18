using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Migrations
{
    public partial class UpdateRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServerUserRole");

            migrationBuilder.AddColumn<Guid>(
                name: "ServerUserId",
                table: "Roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RoleUser",
                columns: table => new
                {
                    RolesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUser", x => new { x.RolesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_RoleUser_Roles_RolesId",
                        column: x => x.RolesId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ServerUserId",
                table: "Roles",
                column: "ServerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleUser_UsersId",
                table: "RoleUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_ServerUsers_ServerUserId",
                table: "Roles",
                column: "ServerUserId",
                principalTable: "ServerUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_ServerUsers_ServerUserId",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "RoleUser");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ServerUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ServerUserId",
                table: "Roles");

            migrationBuilder.CreateTable(
                name: "ServerUserRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ServerUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServerUserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServerUserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServerUserRole_ServerUsers_ServerUserId",
                        column: x => x.ServerUserId,
                        principalTable: "ServerUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServerUserRole_RoleId",
                table: "ServerUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ServerUserRole_ServerUserId",
                table: "ServerUserRole",
                column: "ServerUserId");
        }
    }
}
