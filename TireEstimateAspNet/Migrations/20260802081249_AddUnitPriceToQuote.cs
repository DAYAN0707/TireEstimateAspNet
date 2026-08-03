using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TireEstimateAspNet.Migrations
{
    /// <inheritdoc />
    public partial class AddUnitPriceToQuote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "Quotes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "Quotes");
        }
    }
}
