using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JDev.Tuteee.Api.Migrations
{
    /// <inheritdoc />
    public partial class RemoveLastFourFromAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastFour",
                table: "Account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastFour",
                table: "Account",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
