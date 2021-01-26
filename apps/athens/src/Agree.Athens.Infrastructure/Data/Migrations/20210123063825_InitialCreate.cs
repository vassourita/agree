using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Agree.Athens.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "server",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    avatar_file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_server", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    tag = table.Column<int>(type: "integer", maxLength: 4, nullable: false),
                    status = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    avatar_file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    server_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_category_server_server_id",
                        column: x => x.server_id,
                        principalTable: "server",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "server_user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    server_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_server_user", x => x.id);
                    table.ForeignKey(
                        name: "FK_server_user_server_server_id",
                        column: x => x.server_id,
                        principalTable: "server",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_server_user_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "channel",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<string>(type: "text", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_channel", x => x.id);
                    table.ForeignKey(
                        name: "FK_channel_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    color_hex = table.Column<string>(type: "character(6)", fixedLength: true, maxLength: 6, nullable: false),
                    order = table.Column<int>(type: "integer", nullable: false),
                    permissions_can_create_new_roles = table.Column<bool>(type: "boolean", nullable: false),
                    permissions_can_delete_roles = table.Column<bool>(type: "boolean", nullable: false),
                    permissions_can_delete_server = table.Column<bool>(type: "boolean", nullable: false),
                    permissions_can_remove_users = table.Column<bool>(type: "boolean", nullable: false),
                    permissions_can_update_server_avatar = table.Column<bool>(type: "boolean", nullable: false),
                    permissions_can_update_server_description = table.Column<bool>(type: "boolean", nullable: false),
                    permissions_can_update_server_name = table.Column<bool>(type: "boolean", nullable: false),
                    server_id = table.Column<Guid>(type: "uuid", nullable: false),
                    ServerUserId = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role", x => x.id);
                    table.ForeignKey(
                        name: "FK_role_server_server_id",
                        column: x => x.server_id,
                        principalTable: "server",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_role_server_user_ServerUserId",
                        column: x => x.ServerUserId,
                        principalTable: "server_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "message",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    content = table.Column<string>(type: "text", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    channel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_message", x => x.id);
                    table.ForeignKey(
                        name: "FK_message_channel_channel_id",
                        column: x => x.channel_id,
                        principalTable: "channel",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_message_user_user_id",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_category_server_id",
                table: "category",
                column: "server_id");

            migrationBuilder.CreateIndex(
                name: "IX_channel_category_id",
                table: "channel",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_message_channel_id",
                table: "message",
                column: "channel_id");

            migrationBuilder.CreateIndex(
                name: "IX_message_user_id",
                table: "message",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_server_id",
                table: "role",
                column: "server_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_ServerUserId",
                table: "role",
                column: "ServerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_server_user_server_id",
                table: "server_user",
                column: "server_id");

            migrationBuilder.CreateIndex(
                name: "IX_server_user_user_id",
                table: "server_user",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                table: "user",
                column: "email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "message");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "channel");

            migrationBuilder.DropTable(
                name: "server_user");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "server");
        }
    }
}
