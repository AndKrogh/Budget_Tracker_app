using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BudgetTracker.core.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExpenseReportModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BudgetId",
                table: "ExpenseReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BudgetName",
                table: "ExpenseReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalExpenses",
                table: "ExpenseReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalIncome",
                table: "ExpenseReports",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetId",
                table: "ExpenseReports");

            migrationBuilder.DropColumn(
                name: "BudgetName",
                table: "ExpenseReports");

            migrationBuilder.DropColumn(
                name: "TotalExpenses",
                table: "ExpenseReports");

            migrationBuilder.DropColumn(
                name: "TotalIncome",
                table: "ExpenseReports");
        }
    }
}
