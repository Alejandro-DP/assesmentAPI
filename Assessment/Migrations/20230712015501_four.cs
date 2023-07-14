using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Assessment.Migrations
{
    /// <inheritdoc />
    public partial class four : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Transactions_TransactionsId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionsId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionsId",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionsId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionsId",
                table: "Transactions",
                column: "TransactionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Transactions_TransactionsId",
                table: "Transactions",
                column: "TransactionsId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
