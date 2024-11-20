using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class ModifyNotificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Show",
                table: "Notifications",
                newName: "IsSeen");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Notifications",
                newName: "RequestId");

            migrationBuilder.AddColumn<byte>(
                name: "CategoryType",
                table: "Notifications",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Notifications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Notifications",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedById",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Notifications",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "RequestId",
                table: "Notifications",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "IsSeen",
                table: "Notifications",
                newName: "Show");
        }
    }
}
