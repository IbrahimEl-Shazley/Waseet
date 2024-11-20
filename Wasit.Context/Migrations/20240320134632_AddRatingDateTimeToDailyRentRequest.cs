using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingDateTimeToDailyRentRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RatingDateTime",
                table: "DailyRentRequests",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingDateTime",
                table: "DailyRentRequests");
        }
    }
}
