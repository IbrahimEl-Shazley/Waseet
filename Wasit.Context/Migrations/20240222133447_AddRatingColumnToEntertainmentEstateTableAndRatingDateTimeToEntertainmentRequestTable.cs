using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingColumnToEntertainmentEstateTableAndRatingDateTimeToEntertainmentRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RatingDateTime",
                table: "EntertainmentRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "EntertainmentEstates",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingDateTime",
                table: "EntertainmentRequests");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "EntertainmentEstates");
        }
    }
}
