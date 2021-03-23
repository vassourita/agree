using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agree.Athens.Infrastructure.Data.EntityFramework.Migrations
{
    public partial class AddCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextChannels_Servers_ServerId",
                table: "TextChannels");

            migrationBuilder.RenameColumn(
                name: "ServerId",
                table: "TextChannels",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TextChannels_ServerId",
                table: "TextChannels",
                newName: "IX_TextChannels_CategoryId");

            migrationBuilder.CreateTable(
                name: "CategoryDbModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    ServerId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDbModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryDbModel_Servers_ServerId",
                        column: x => x.ServerId,
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryDbModel_ServerId",
                table: "CategoryDbModel",
                column: "ServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TextChannels_CategoryDbModel_CategoryId",
                table: "TextChannels",
                column: "CategoryId",
                principalTable: "CategoryDbModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextChannels_CategoryDbModel_CategoryId",
                table: "TextChannels");

            migrationBuilder.DropTable(
                name: "CategoryDbModel");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "TextChannels",
                newName: "ServerId");

            migrationBuilder.RenameIndex(
                name: "IX_TextChannels_CategoryId",
                table: "TextChannels",
                newName: "IX_TextChannels_ServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_TextChannels_Servers_ServerId",
                table: "TextChannels",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
