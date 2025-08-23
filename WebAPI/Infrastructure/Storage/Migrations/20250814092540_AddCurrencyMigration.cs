using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Storage.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Type"},
                values: new object[,]
                {
                    { "RUB" },
                    { "USD" },
                    { "EUR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Type",
                keyValues: new object[] { "RUB" });

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Type",
                keyValues: new object[] { "USD" });

            migrationBuilder.DeleteData(
                table: "Currency",
                keyColumn: "Type",
                keyValues: new object[] { "EUR" });
        }
    }
}
