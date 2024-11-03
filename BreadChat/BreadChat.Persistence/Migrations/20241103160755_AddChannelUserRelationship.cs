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

            migrationBuilder.DropColumn(
                name: "ChannelDbEntityId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "UserChannels",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ChannelId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChannels", x => new { x.UserId, x.ChannelId });
                    table.ForeignKey(
                        name: "FK_UserChannels_Channels_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChannels_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserChannels_ChannelId",
                table: "UserChannels",
                column: "ChannelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserChannels");

            migrationBuilder.AddColumn<Guid>(
                name: "ChannelDbEntityId",
                table: "Users",
                type: "TEXT",
                nullable: true);

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
