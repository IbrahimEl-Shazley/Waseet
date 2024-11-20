using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddRejectionReasonToEstateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "SaleEstates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "RentEstates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "EntertainmentEstates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectionReason",
                table: "DailyRentEstates",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "SaleEstates");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "RentEstates");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "EntertainmentEstates");

            migrationBuilder.DropColumn(
                name: "RejectionReason",
                table: "DailyRentEstates");
        }
    }
}
