using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JDev.Tuteee.Rest.Api.Migrations
{
    /// <inheritdoc />
    public partial class TuteeRoleAndClientRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Client_ClientId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Tutee_TuteeId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Rate_Tutee_TuteeId",
                table: "Rate");

            migrationBuilder.DropTable(
                name: "Tutee");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_TuteeId",
                table: "Lesson");

            migrationBuilder.RenameColumn(
                name: "TuteeId",
                table: "Rate",
                newName: "TuteeRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Rate_TuteeId",
                table: "Rate",
                newName: "IX_Rate_TuteeRoleId");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Invoice",
                newName: "ClientRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_ClientId",
                table: "Invoice",
                newName: "IX_Invoice_ClientRoleId");

            migrationBuilder.AddColumn<int>(
                name: "TuteeRoleId",
                table: "Lesson",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TuitionStakeholder",
                columns: table => new
                {
                    TuitionStakeholderId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TuitionStakeholder", x => x.TuitionStakeholderId);
                });

            migrationBuilder.CreateTable(
                name: "ClientRole",
                columns: table => new
                {
                    ClientRoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TuitionStakeholderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRole", x => x.ClientRoleId);
                    table.ForeignKey(
                        name: "FK_ClientRole_TuitionStakeholder_TuitionStakeholderId",
                        column: x => x.TuitionStakeholderId,
                        principalTable: "TuitionStakeholder",
                        principalColumn: "TuitionStakeholderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TuteeRole",
                columns: table => new
                {
                    TuteeRoleId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientRoleId = table.Column<int>(type: "integer", nullable: false),
                    TuitionStakeholderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TuteeRole", x => x.TuteeRoleId);
                    table.ForeignKey(
                        name: "FK_TuteeRole_ClientRole_ClientRoleId",
                        column: x => x.ClientRoleId,
                        principalTable: "ClientRole",
                        principalColumn: "ClientRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TuteeRole_TuitionStakeholder_TuitionStakeholderId",
                        column: x => x.TuitionStakeholderId,
                        principalTable: "TuitionStakeholder",
                        principalColumn: "TuitionStakeholderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_TuteeRoleId",
                table: "Lesson",
                column: "TuteeRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRole_TuitionStakeholderId",
                table: "ClientRole",
                column: "TuitionStakeholderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TuteeRole_ClientRoleId",
                table: "TuteeRole",
                column: "ClientRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TuteeRole_TuitionStakeholderId",
                table: "TuteeRole",
                column: "TuitionStakeholderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_ClientRole_ClientRoleId",
                table: "Invoice",
                column: "ClientRoleId",
                principalTable: "ClientRole",
                principalColumn: "ClientRoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_TuteeRole_TuteeRoleId",
                table: "Lesson",
                column: "TuteeRoleId",
                principalTable: "TuteeRole",
                principalColumn: "TuteeRoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_TuteeRole_TuteeRoleId",
                table: "Rate",
                column: "TuteeRoleId",
                principalTable: "TuteeRole",
                principalColumn: "TuteeRoleId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_ClientRole_ClientRoleId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_TuteeRole_TuteeRoleId",
                table: "Lesson");

            migrationBuilder.DropForeignKey(
                name: "FK_Rate_TuteeRole_TuteeRoleId",
                table: "Rate");

            migrationBuilder.DropTable(
                name: "TuteeRole");

            migrationBuilder.DropTable(
                name: "ClientRole");

            migrationBuilder.DropTable(
                name: "TuitionStakeholder");

            migrationBuilder.DropIndex(
                name: "IX_Lesson_TuteeRoleId",
                table: "Lesson");

            migrationBuilder.DropColumn(
                name: "TuteeRoleId",
                table: "Lesson");

            migrationBuilder.RenameColumn(
                name: "TuteeRoleId",
                table: "Rate",
                newName: "TuteeId");

            migrationBuilder.RenameIndex(
                name: "IX_Rate_TuteeRoleId",
                table: "Rate",
                newName: "IX_Rate_TuteeId");

            migrationBuilder.RenameColumn(
                name: "ClientRoleId",
                table: "Invoice",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_ClientRoleId",
                table: "Invoice",
                newName: "IX_Invoice_ClientId");

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    HolderFirstName = table.Column<string>(type: "text", nullable: false),
                    HolderLastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "Tutee",
                columns: table => new
                {
                    TuteeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutee", x => x.TuteeId);
                    table.ForeignKey(
                        name: "FK_Tutee_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_TuteeId",
                table: "Lesson",
                column: "TuteeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tutee_ClientId",
                table: "Tutee",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Client_ClientId",
                table: "Invoice",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Tutee_TuteeId",
                table: "Lesson",
                column: "TuteeId",
                principalTable: "Tutee",
                principalColumn: "TuteeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rate_Tutee_TuteeId",
                table: "Rate",
                column: "TuteeId",
                principalTable: "Tutee",
                principalColumn: "TuteeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
