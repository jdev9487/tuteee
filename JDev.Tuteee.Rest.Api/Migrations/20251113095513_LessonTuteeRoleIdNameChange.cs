using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JDev.Tuteee.Rest.Api.Migrations
{
    /// <inheritdoc />
    public partial class LessonTuteeRoleIdNameChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TuteeId",
                table: "Lesson");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TuteeId",
                table: "Lesson",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
