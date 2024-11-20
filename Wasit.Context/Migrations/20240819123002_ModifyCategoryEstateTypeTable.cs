using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class ModifyCategoryEstateTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEstateTypes_Categories_CategoryId1",
                table: "CategoryEstateTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEstateTypes_EstateTypes_EstateTypeId1",
                table: "CategoryEstateTypes");

            migrationBuilder.DropIndex(
                name: "IX_CategoryEstateTypes_CategoryId1",
                table: "CategoryEstateTypes");

            migrationBuilder.DropIndex(
                name: "IX_CategoryEstateTypes_EstateTypeId1",
                table: "CategoryEstateTypes");

            migrationBuilder.DropColumn(
                name: "CategoryId1",
                table: "CategoryEstateTypes");

            migrationBuilder.DropColumn(
                name: "EstateTypeId1",
                table: "CategoryEstateTypes");

            migrationBuilder.AlterColumn<long>(
                name: "EstateTypeId",
                table: "CategoryEstateTypes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "CategoryId",
                table: "CategoryEstateTypes",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEstateTypes_CategoryId",
                table: "CategoryEstateTypes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEstateTypes_EstateTypeId",
                table: "CategoryEstateTypes",
                column: "EstateTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEstateTypes_Categories_CategoryId",
                table: "CategoryEstateTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEstateTypes_EstateTypes_EstateTypeId",
                table: "CategoryEstateTypes",
                column: "EstateTypeId",
                principalTable: "EstateTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEstateTypes_Categories_CategoryId",
                table: "CategoryEstateTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryEstateTypes_EstateTypes_EstateTypeId",
                table: "CategoryEstateTypes");

            migrationBuilder.DropIndex(
                name: "IX_CategoryEstateTypes_CategoryId",
                table: "CategoryEstateTypes");

            migrationBuilder.DropIndex(
                name: "IX_CategoryEstateTypes_EstateTypeId",
                table: "CategoryEstateTypes");

            migrationBuilder.AlterColumn<int>(
                name: "EstateTypeId",
                table: "CategoryEstateTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "CategoryEstateTypes",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "CategoryId1",
                table: "CategoryEstateTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EstateTypeId1",
                table: "CategoryEstateTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEstateTypes_CategoryId1",
                table: "CategoryEstateTypes",
                column: "CategoryId1");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryEstateTypes_EstateTypeId1",
                table: "CategoryEstateTypes",
                column: "EstateTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEstateTypes_Categories_CategoryId1",
                table: "CategoryEstateTypes",
                column: "CategoryId1",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryEstateTypes_EstateTypes_EstateTypeId1",
                table: "CategoryEstateTypes",
                column: "EstateTypeId1",
                principalTable: "EstateTypes",
                principalColumn: "Id");
        }
    }
}
