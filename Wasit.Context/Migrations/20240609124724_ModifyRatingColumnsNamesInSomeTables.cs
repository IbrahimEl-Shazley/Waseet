using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class ModifyRatingColumnsNamesInSomeTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRate",
                table: "SaleRatingRequests",
                newName: "UserRating");

            migrationBuilder.RenameColumn(
                name: "UserRate",
                table: "RentRequests",
                newName: "OwnerRating");

            migrationBuilder.RenameColumn(
                name: "UserRate",
                table: "RentRatingRequests",
                newName: "ReviewerRating");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRating",
                table: "SaleRatingRequests",
                newName: "UserRate");

            migrationBuilder.RenameColumn(
                name: "OwnerRating",
                table: "RentRequests",
                newName: "UserRate");

            migrationBuilder.RenameColumn(
                name: "ReviewerRating",
                table: "RentRatingRequests",
                newName: "UserRate");
        }
    }
}
