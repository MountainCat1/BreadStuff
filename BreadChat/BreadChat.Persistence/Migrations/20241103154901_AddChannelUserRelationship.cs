using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreadChat.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddChannelUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Channels_ChannelDbEntityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChannelDbEntityId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Channels",
                table: "Channels");

            migrationBuilder.DropColumn(
                name: "ChannelDbEntityId",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Channels",
                newName: "UserChannels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChannels",
                table: "UserChannels",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserChannelDbEntity",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChannelId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChannelDbEntityId = table.Column<Guid>(type: "TEXT", nullable: false),
                    UsersId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChannelDbEntity", x => new { x.UserId, x.ChannelId });
                    table.ForeignKey(
                        name: "FK_UserChannelDbEntity_UserChannels_ChannelDbEntityId",
                        column: x => x.ChannelDbEntityId,
                        principalTable: "UserChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChannelDbEntity_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserChannelDbEntity_ChannelDbEntityId",
                table: "UserChannelDbEntity",
                column: "ChannelDbEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChannelDbEntity_UsersId",
                table: "UserChannelDbEntity",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserChannelDbEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChannels",
                table: "UserChannels");

            migrationBuilder.RenameTable(
                name: "UserChannels",
                newName: "Channels");

            migrationBuilder.AddColumn<Guid>(
                name: "ChannelDbEntityId",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Channels",
                table: "Channels",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ChannelDbEntityId",
                table: "Users",
                column: "ChannelDbEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Channels_ChannelDbEntityId",
                table: "Users",
                column: "ChannelDbEntityId",
                principalTable: "Channels",
                principalColumn: "Id");
        }
    }
}
