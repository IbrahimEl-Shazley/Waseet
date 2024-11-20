using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingPropsToEntertainmentRequestTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRate",
                table: "EntertainmentRequests",
                newName: "UserRating");

            migrationBuilder.RenameColumn(
                name: "RatingDateTime",
                table: "EntertainmentRequests",
                newName: "UserRatingDateTime");

            migrationBuilder.RenameColumn(
                name: "EstateRate",
                table: "EntertainmentRequests",
                newName: "OwnerRating");

            migrationBuilder.AddColumn<double>(
                name: "EstateRating",
                table: "EntertainmentRequests",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "OwnerRatingDateTime",
                table: "EntertainmentRequests",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstateRating",
                table: "EntertainmentRequests");

            migrationBuilder.DropColumn(
                name: "OwnerRatingDateTime",
                table: "EntertainmentRequests");

            migrationBuilder.RenameColumn(
                name: "UserRatingDateTime",
                table: "EntertainmentRequests",
                newName: "RatingDateTime");

            migrationBuilder.RenameColumn(
                name: "UserRating",
                table: "EntertainmentRequests",
                newName: "UserRate");

            migrationBuilder.RenameColumn(
                name: "OwnerRating",
                table: "EntertainmentRequests",
                newName: "EstateRate");
        }
    }
}
