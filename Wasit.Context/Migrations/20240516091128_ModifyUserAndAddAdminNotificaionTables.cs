using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class ModifyUserAndAddAdminNotificaionTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "Notifications");

            migrationBuilder.RenameColumn(
                name: "SmsProvider",
                table: "Settings",
                newName: "UpdatedById");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Settings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Settings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Settings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Settings",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RouteId",
                table: "Notifications",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Lang",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdminNotifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TextAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    IsSeen = table.Column<bool>(type: "bit", nullable: false),
                    RouteId = table.Column<long>(type: "bigint", nullable: true),
                    CategoryType = table.Column<byte>(type: "tinyint", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminNotifications", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminNotifications");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "RouteId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Lang",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UpdatedById",
                table: "Settings",
                newName: "SmsProvider");

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "Notifications",
                type: "int",
                nullable: true);
        }
    }
}
