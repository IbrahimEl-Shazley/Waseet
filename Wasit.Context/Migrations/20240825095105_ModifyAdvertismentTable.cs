using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class ModifyAdvertismentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "TitleAr",
                table: "Advertisments");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Advertisments");

            migrationBuilder.RenameColumn(
                name: "TitleEn",
                table: "Advertisments",
                newName: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Advertisments",
                newName: "TitleEn");

            migrationBuilder.AddColumn<string>(
                name: "Img",
                table: "Advertisments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleAr",
                table: "Advertisments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Advertisments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
