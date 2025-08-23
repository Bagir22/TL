using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Storage.Migrations
{
    /// <inheritdoc />
    public partial class FixReservationQuest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationGuest_Reservation_ReservationId",
                table: "ReservationGuest");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationGuest_Reservation_ReservationId",
                table: "ReservationGuest",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationGuest_Reservation_ReservationId",
                table: "ReservationGuest");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationGuest_Reservation_ReservationId",
                table: "ReservationGuest",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
