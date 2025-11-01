using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Emocare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TaskModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "WellnessTasks");

            migrationBuilder.RenameColumn(
                name: "MoodTags",
                table: "WellnessTasks",
                newName: "OwnerType");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WellnessTasks",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WellnessTasks",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByPsychologistId",
                table: "WellnessTasks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MoodTag",
                table: "WellnessTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PsychologistTaskAssignments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PsychologistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellnessTaskId = table.Column<int>(type: "int", nullable: false),
                    Frequency = table.Column<int>(type: "int", nullable: false),
                    AssignedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologistTaskAssignments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsychologistTaskAssignments_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PsychologistTaskAssignments_Users_PsychologistId",
                        column: x => x.PsychologistId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PsychologistTaskAssignments_WellnessTasks_WellnessTaskId",
                        column: x => x.WellnessTaskId,
                        principalTable: "WellnessTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDailyTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WellnessTaskId = table.Column<int>(type: "int", nullable: false),
                    DateAssigned = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDailyTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDailyTasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDailyTasks_WellnessTasks_WellnessTaskId",
                        column: x => x.WellnessTaskId,
                        principalTable: "WellnessTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserStreaks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrentStreak = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserStreaks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserStreaks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserDailyTaskByPsychologists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PsychologistTaskAssignmentId = table.Column<int>(type: "int", nullable: false),
                    DateAssigned = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDailyTaskByPsychologists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDailyTaskByPsychologists_PsychologistTaskAssignments_PsychologistTaskAssignmentId",
                        column: x => x.PsychologistTaskAssignmentId,
                        principalTable: "PsychologistTaskAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WellnessTasks_CreatedByPsychologistId",
                table: "WellnessTasks",
                column: "CreatedByPsychologistId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologistTaskAssignments_ClientId",
                table: "PsychologistTaskAssignments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologistTaskAssignments_PsychologistId",
                table: "PsychologistTaskAssignments",
                column: "PsychologistId");

            migrationBuilder.CreateIndex(
                name: "IX_PsychologistTaskAssignments_WellnessTaskId",
                table: "PsychologistTaskAssignments",
                column: "WellnessTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDailyTaskByPsychologists_PsychologistTaskAssignmentId",
                table: "UserDailyTaskByPsychologists",
                column: "PsychologistTaskAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDailyTasks_UserId_WellnessTaskId_DateAssigned",
                table: "UserDailyTasks",
                columns: new[] { "UserId", "WellnessTaskId", "DateAssigned" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDailyTasks_WellnessTaskId",
                table: "UserDailyTasks",
                column: "WellnessTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UserStreaks_UserId",
                table: "UserStreaks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WellnessTasks_Users_CreatedByPsychologistId",
                table: "WellnessTasks",
                column: "CreatedByPsychologistId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WellnessTasks_Users_CreatedByPsychologistId",
                table: "WellnessTasks");

            migrationBuilder.DropTable(
                name: "UserDailyTaskByPsychologists");

            migrationBuilder.DropTable(
                name: "UserDailyTasks");

            migrationBuilder.DropTable(
                name: "UserStreaks");

            migrationBuilder.DropTable(
                name: "PsychologistTaskAssignments");

            migrationBuilder.DropIndex(
                name: "IX_WellnessTasks_CreatedByPsychologistId",
                table: "WellnessTasks");

            migrationBuilder.DropColumn(
                name: "CreatedByPsychologistId",
                table: "WellnessTasks");

            migrationBuilder.DropColumn(
                name: "MoodTag",
                table: "WellnessTasks");

            migrationBuilder.RenameColumn(
                name: "OwnerType",
                table: "WellnessTasks",
                newName: "MoodTags");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WellnessTasks",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "WellnessTasks",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "WellnessTasks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
