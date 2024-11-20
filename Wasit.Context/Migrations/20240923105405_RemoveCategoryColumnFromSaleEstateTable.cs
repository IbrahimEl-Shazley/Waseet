using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCategoryColumnFromSaleEstateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "SaleEstates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Category",
                table: "SaleEstates",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
