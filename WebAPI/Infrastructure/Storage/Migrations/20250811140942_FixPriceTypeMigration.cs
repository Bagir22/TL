using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Storage.Migrations
{
    /// <inheritdoc />
    public partial class FixPriceTypeMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DailyPrice",
                table: "RoomType",
                type: "decimal(9,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,7)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Reservation",
                type: "decimal(9,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,7)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DailyPrice",
                table: "RoomType",
                type: "decimal(9,7)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Reservation",
                type: "decimal(9,7)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)");
        }
    }
}
