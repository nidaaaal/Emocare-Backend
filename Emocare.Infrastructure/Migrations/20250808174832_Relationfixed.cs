using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emocare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Relationfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChatMessages_Users_ReceiverId",
                table: "UserChatMessages");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChatMessages_Users_ReceiverId",
                table: "UserChatMessages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChatMessages_Users_ReceiverId",
                table: "UserChatMessages");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChatMessages_Users_ReceiverId",
                table: "UserChatMessages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
