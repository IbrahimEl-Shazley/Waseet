using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class ModifyContactUsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactUs_ContactUsSubjects_SubjectId",
                table: "ContactUs");

            migrationBuilder.DropTable(
                name: "ContactUsSubjects");

            migrationBuilder.DropIndex(
                name: "IX_ContactUs_SubjectId",
                table: "ContactUs");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "ContactUs");

            migrationBuilder.AddColumn<int>(
                name: "Subject",
                table: "ContactUs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "ContactUs");

            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                table: "ContactUs",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ContactUsSubjects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    NameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UpdatedById = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactUsSubjects", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactUs_SubjectId",
                table: "ContactUs",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactUs_ContactUsSubjects_SubjectId",
                table: "ContactUs",
                column: "SubjectId",
                principalTable: "ContactUsSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
