using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JDev.Tuteee.Rest.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationSlots",
                columns: table => new
                {
                    ReservationSlotId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TuteeRoleId = table.Column<int>(type: "integer", nullable: false),
                    ReferenceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationSlots", x => x.ReservationSlotId);
                    table.ForeignKey(
                        name: "FK_ReservationSlots_TuteeRole_TuteeRoleId",
                        column: x => x.TuteeRoleId,
                        principalTable: "TuteeRole",
                        principalColumn: "TuteeRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservationSlots_TuteeRoleId",
                table: "ReservationSlots",
                column: "TuteeRoleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationSlots");
        }
    }
}
