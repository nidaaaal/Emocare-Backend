using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emocare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ImageAddedToTask : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "WellnessTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "WellnessTasks");
        }
    }
}
