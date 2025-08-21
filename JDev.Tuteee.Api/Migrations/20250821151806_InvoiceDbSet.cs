using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JDev.Tuteee.Api.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Client_ClientId",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Invoice_InvoiceId",
                table: "Lesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.RenameTable(
                name: "Invoice",
                newName: "Invoices");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_ClientId",
                table: "Invoices",
                newName: "IX_Invoices_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Client_ClientId",
                table: "Invoices",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Invoices_InvoiceId",
                table: "Lesson",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "InvoiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Client_ClientId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Invoices_InvoiceId",
                table: "Lesson");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoices",
                table: "Invoices");

            migrationBuilder.RenameTable(
                name: "Invoices",
                newName: "Invoice");

            migrationBuilder.RenameIndex(
                name: "IX_Invoices_ClientId",
                table: "Invoice",
                newName: "IX_Invoice_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Client_ClientId",
                table: "Invoice",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Invoice_InvoiceId",
                table: "Lesson",
                column: "InvoiceId",
                principalTable: "Invoice",
                principalColumn: "InvoiceId");
        }
    }
}
