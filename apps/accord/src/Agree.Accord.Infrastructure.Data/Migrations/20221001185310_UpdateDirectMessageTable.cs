using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agree.Accord.Infrastructure.Data.Migrations
{
    public partial class UpdateDirectMessageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_UserAccounts_FromId",
                table: "DirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_UserAccounts_ToId",
                table: "DirectMessages");

            migrationBuilder.AlterColumn<Guid>(
                name: "ToId",
                table: "DirectMessages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "FromId",
                table: "DirectMessages",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "InReplyToId",
                table: "DirectMessages",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_InReplyToId",
                table: "DirectMessages",
                column: "InReplyToId");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_DirectMessages_InReplyToId",
                table: "DirectMessages",
                column: "InReplyToId",
                principalTable: "DirectMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_UserAccounts_FromId",
                table: "DirectMessages",
                column: "FromId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_UserAccounts_ToId",
                table: "DirectMessages",
                column: "ToId",
                principalTable: "UserAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_DirectMessages_InReplyToId",
                table: "DirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_UserAccounts_FromId",
                table: "DirectMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_DirectMessages_UserAccounts_ToId",
                table: "DirectMessages");

            migrationBuilder.DropIndex(
                name: "IX_DirectMessages_InReplyToId",
                table: "DirectMessages");

            migrationBuilder.DropColumn(
                name: "InReplyToId",
                table: "DirectMessages");

            migrationBuilder.AlterColumn<Guid>(
                name: "ToId",
                table: "DirectMessages",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "FromId",
                table: "DirectMessages",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_UserAccounts_FromId",
                table: "DirectMessages",
                column: "FromId",
                principalTable: "UserAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DirectMessages_UserAccounts_ToId",
                table: "DirectMessages",
                column: "ToId",
                principalTable: "UserAccounts",
                principalColumn: "Id");
        }
    }
}
