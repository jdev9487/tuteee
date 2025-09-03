using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JDev.Tuteee.Rest.Api.Migrations
{
    /// <inheritdoc />
    public partial class HomeworkAttachments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HomeworkAttachment",
                columns: table => new
                {
                    HomeworkAttachmentId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Path = table.Column<string>(type: "TEXT", nullable: false),
                    LessonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkAttachment", x => x.HomeworkAttachmentId);
                    table.ForeignKey(
                        name: "FK_HomeworkAttachment_Lesson_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lesson",
                        principalColumn: "LessonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkAttachment_LessonId",
                table: "HomeworkAttachment",
                column: "LessonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HomeworkAttachment");
        }
    }
}
