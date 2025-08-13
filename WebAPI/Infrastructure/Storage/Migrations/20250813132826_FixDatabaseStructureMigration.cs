using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Storage.Migrations
{
    /// <inheritdoc />
    public partial class FixDatabaseStructureMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomType_Property_PropertyId",
                table: "RoomType");

            migrationBuilder.DropColumn(
                name: "Amenities",
                table: "RoomType");

            migrationBuilder.DropColumn(
                name: "Services",
                table: "RoomType");

            migrationBuilder.DropColumn(
                name: "GuestName",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "GuestPhoneNumber",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "RoomType",
                newName: "CurrencyId");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Reservation",
                newName: "CurrencyId");

            migrationBuilder.CreateTable(
                name: "Amenity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Guest",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Guest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypeAmenity",
                columns: table => new
                {
                    RoomTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmenityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypeAmenity", x => new { x.RoomTypeId, x.AmenityId });
                    table.ForeignKey(
                        name: "FK_RoomTypeAmenity_Amenity_AmenityId",
                        column: x => x.AmenityId,
                        principalTable: "Amenity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomTypeAmenity_RoomType_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReservationGuest",
                columns: table => new
                {
                    ReservationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GuestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationGuest", x => new { x.ReservationId, x.GuestId });
                    table.ForeignKey(
                        name: "FK_ReservationGuest_Guest_GuestId",
                        column: x => x.GuestId,
                        principalTable: "Guest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReservationGuest_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypeService",
                columns: table => new
                {
                    RoomTypeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypeService", x => new { x.RoomTypeId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_RoomTypeService_RoomType_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomTypeService_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoomType_CurrencyId",
                table: "RoomType",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_CurrencyId",
                table: "Reservation",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationGuest_GuestId",
                table: "ReservationGuest",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypeAmenity_AmenityId",
                table: "RoomTypeAmenity",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTypeService_ServiceId",
                table: "RoomTypeService",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Currency_CurrencyId",
                table: "Reservation",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomType_Currency_CurrencyId",
                table: "RoomType",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomType_Property_PropertyId",
                table: "RoomType",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Currency_CurrencyId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomType_Currency_CurrencyId",
                table: "RoomType");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomType_Property_PropertyId",
                table: "RoomType");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "ReservationGuest");

            migrationBuilder.DropTable(
                name: "RoomTypeAmenity");

            migrationBuilder.DropTable(
                name: "RoomTypeService");

            migrationBuilder.DropTable(
                name: "Guest");

            migrationBuilder.DropTable(
                name: "Amenity");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropIndex(
                name: "IX_RoomType_CurrencyId",
                table: "RoomType");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_CurrencyId",
                table: "Reservation");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "RoomType",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "CurrencyId",
                table: "Reservation",
                newName: "Currency");

            migrationBuilder.AddColumn<string>(
                name: "Amenities",
                table: "RoomType",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Services",
                table: "RoomType",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuestName",
                table: "Reservation",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GuestPhoneNumber",
                table: "Reservation",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomType_Property_PropertyId",
                table: "RoomType",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
