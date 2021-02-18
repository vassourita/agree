using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Migrations
{
    public partial class UpdateRelationsAndColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_ServerUsers_ServerUserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_ServerUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "ServerUserId",
                table: "Roles");

            migrationBuilder.AddColumn<bool>(
                name: "EmailVerified",
                table: "Users",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailVerified",
                table: "Users");

            migrationBuilder.AddColumn<Guid>(
                name: "ServerUserId",
                table: "Roles",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ServerUserId",
                table: "Roles",
                column: "ServerUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_ServerUsers_ServerUserId",
                table: "Roles",
                column: "ServerUserId",
                principalTable: "ServerUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
