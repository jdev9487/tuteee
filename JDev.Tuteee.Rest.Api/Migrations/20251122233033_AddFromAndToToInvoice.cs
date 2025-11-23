using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JDev.Tuteee.Rest.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFromAndToToInvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Invoice");

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Lesson",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateOnly>(
                name: "From",
                table: "Invoice",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "To",
                table: "Invoice",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Paid",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "From",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "To",
                table: "Invoice");

            migrationBuilder.AddColumn<bool>(
                name: "Paid",
                table: "Invoice",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
