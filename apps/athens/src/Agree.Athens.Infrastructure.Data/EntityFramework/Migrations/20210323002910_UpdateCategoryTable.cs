using Microsoft.EntityFrameworkCore.Migrations;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Migrations
{
    public partial class UpdateCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryDbModel_Servers_ServerId",
                table: "CategoryDbModel");

            migrationBuilder.DropForeignKey(
                name: "FK_TextChannels_CategoryDbModel_CategoryId",
                table: "TextChannels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryDbModel",
                table: "CategoryDbModel");

            migrationBuilder.RenameTable(
                name: "CategoryDbModel",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryDbModel_ServerId",
                table: "Category",
                newName: "IX_Category_ServerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Category",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Servers_ServerId",
                table: "Category",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TextChannels_Category_CategoryId",
                table: "TextChannels",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Servers_ServerId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_TextChannels_Category_CategoryId",
                table: "TextChannels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "CategoryDbModel");

            migrationBuilder.RenameIndex(
                name: "IX_Category_ServerId",
                table: "CategoryDbModel",
                newName: "IX_CategoryDbModel_ServerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "CategoryDbModel",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryDbModel",
                table: "CategoryDbModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryDbModel_Servers_ServerId",
                table: "CategoryDbModel",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TextChannels_CategoryDbModel_CategoryId",
                table: "TextChannels",
                column: "CategoryId",
                principalTable: "CategoryDbModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
