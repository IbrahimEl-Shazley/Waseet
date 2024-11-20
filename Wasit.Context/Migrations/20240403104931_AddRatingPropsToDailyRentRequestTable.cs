using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingPropsToDailyRentRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRate",
                table: "DailyRentRequests",
                newName: "UserRating");

            migrationBuilder.RenameColumn(
                name: "RatingDateTime",
                table: "DailyRentRequests",
                newName: "UserRatingDateTime");

            migrationBuilder.RenameColumn(
                name: "EstateRate",
                table: "DailyRentRequests",
                newName: "OwnerRating");

            migrationBuilder.AddColumn<double>(
                name: "EstateRating",
                table: "DailyRentRequests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OwnerRatingDateTime",
                table: "DailyRentRequests",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstateRating",
                table: "DailyRentRequests");

            migrationBuilder.DropColumn(
                name: "OwnerRatingDateTime",
                table: "DailyRentRequests");

            migrationBuilder.RenameColumn(
                name: "UserRatingDateTime",
                table: "DailyRentRequests",
                newName: "RatingDateTime");

            migrationBuilder.RenameColumn(
                name: "UserRating",
                table: "DailyRentRequests",
                newName: "UserRate");

            migrationBuilder.RenameColumn(
                name: "OwnerRating",
                table: "DailyRentRequests",
                newName: "EstateRate");
        }
    }
}
