using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Agree.Athens.Infrastructure.Data.Migrations
{
    public partial class UpdateRoleJoinsWithMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServerUserRole_role_ServerUserId",
                table: "ServerUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_ServerUserRole_server_user_ServerUserId",
                table: "ServerUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServerUserRole",
                table: "ServerUserRole");

            migrationBuilder.RenameTable(
                name: "ServerUserRole",
                newName: "server_user_role");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "server_user_role",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "server_user_role",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "server_user_role",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ServerUserId",
                table: "server_user_role",
                newName: "server_user_id");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "server_user_role",
                newName: "role_id");

            migrationBuilder.RenameIndex(
                name: "IX_ServerUserRole_ServerUserId",
                table: "server_user_role",
                newName: "IX_server_user_role_server_user_id");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "server_user_role",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_server_user_role",
                table: "server_user_role",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_server_user_role_role_id",
                table: "server_user_role",
                column: "role_id");

            migrationBuilder.AddForeignKey(
                name: "FK_server_user_role_role_role_id",
                table: "server_user_role",
                column: "role_id",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_server_user_role_server_user_server_user_id",
                table: "server_user_role",
                column: "server_user_id",
                principalTable: "server_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_server_user_role_role_role_id",
                table: "server_user_role");

            migrationBuilder.DropForeignKey(
                name: "FK_server_user_role_server_user_server_user_id",
                table: "server_user_role");

            migrationBuilder.DropPrimaryKey(
                name: "PK_server_user_role",
                table: "server_user_role");

            migrationBuilder.DropIndex(
                name: "IX_server_user_role_role_id",
                table: "server_user_role");

            migrationBuilder.RenameTable(
                name: "server_user_role",
                newName: "ServerUserRole");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ServerUserRole",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "ServerUserRole",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "server_user_id",
                table: "ServerUserRole",
                newName: "ServerUserId");

            migrationBuilder.RenameColumn(
                name: "role_id",
                table: "ServerUserRole",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "ServerUserRole",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_server_user_role_server_user_id",
                table: "ServerUserRole",
                newName: "IX_ServerUserRole_ServerUserId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "ServerUserRole",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServerUserRole",
                table: "ServerUserRole",
                columns: new[] { "RoleId", "ServerUserId", "Id" });

            migrationBuilder.AddForeignKey(
                name: "FK_ServerUserRole_role_ServerUserId",
                table: "ServerUserRole",
                column: "ServerUserId",
                principalTable: "role",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServerUserRole_server_user_ServerUserId",
                table: "ServerUserRole",
                column: "ServerUserId",
                principalTable: "server_user",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
