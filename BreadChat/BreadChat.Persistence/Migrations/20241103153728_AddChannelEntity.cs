using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreadChat.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddChannelEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ChannelDbEntityId",
                table: "Users",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Channels_ChannelDbEntityId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropIndex(
                name: "IX_Users_ChannelDbEntityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ChannelDbEntityId",
                table: "Users");
        }
    }
}
