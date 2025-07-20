using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JDev.Tuteee.Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameGuardianToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tutee_Guardian_GuardianId",
                table: "Tutee");

            migrationBuilder.DropTable(
                name: "Guardian");

            migrationBuilder.RenameColumn(
                name: "GuardianId",
                table: "Tutee",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Tutee_GuardianId",
                table: "Tutee",
                newName: "IX_Tutee_AccountId");

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HolderFirstName = table.Column<string>(type: "TEXT", nullable: false),
                    HolderLastName = table.Column<string>(type: "TEXT", nullable: false),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tutee_Account_AccountId",
                table: "Tutee",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tutee_Account_AccountId",
                table: "Tutee");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Tutee",
                newName: "GuardianId");

            migrationBuilder.RenameIndex(
                name: "IX_Tutee_AccountId",
                table: "Tutee",
                newName: "IX_Tutee_GuardianId");

            migrationBuilder.CreateTable(
                name: "Guardian",
                columns: table => new
                {
                    GuardianId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EmailAddress = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guardian", x => x.GuardianId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Tutee_Guardian_GuardianId",
                table: "Tutee",
                column: "GuardianId",
                principalTable: "Guardian",
                principalColumn: "GuardianId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
