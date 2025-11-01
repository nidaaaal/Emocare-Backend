using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emocare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class WellnessModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "PsychologistTaskAssignments");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "WellnessTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "WellnessTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "EstimatedDurationMinutes",
                table: "WellnessTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "WellnessTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRecommended",
                table: "WellnessTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "WellnessTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SuccessRate",
                table: "WellnessTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalCompletions",
                table: "WellnessTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "WellnessTasks");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "WellnessTasks");

            migrationBuilder.DropColumn(
                name: "EstimatedDurationMinutes",
                table: "WellnessTasks");

            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "WellnessTasks");

            migrationBuilder.DropColumn(
                name: "IsRecommended",
                table: "WellnessTasks");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "WellnessTasks");

            migrationBuilder.DropColumn(
                name: "SuccessRate",
                table: "WellnessTasks");

            migrationBuilder.DropColumn(
                name: "TotalCompletions",
                table: "WellnessTasks");

            migrationBuilder.AddColumn<int>(
                name: "Frequency",
                table: "PsychologistTaskAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
