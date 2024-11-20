using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wasit.Context.Migrations
{
    /// <inheritdoc />
    public partial class ModifySettingsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserNameSms",
                table: "Settings",
                newName: "UserName_SMS");

            migrationBuilder.RenameColumn(
                name: "SenderName",
                table: "Settings",
                newName: "ServerKey_FCM");

            migrationBuilder.RenameColumn(
                name: "SenderId",
                table: "Settings",
                newName: "SenderName_SMS");

            migrationBuilder.RenameColumn(
                name: "PasswordSms",
                table: "Settings",
                newName: "SenderId_FCM");

            migrationBuilder.RenameColumn(
                name: "CondtionsEnProvider",
                table: "Settings",
                newName: "Password_SMS");

            migrationBuilder.RenameColumn(
                name: "CondtionsEnClient",
                table: "Settings",
                newName: "CondtionsOwnerEn");

            migrationBuilder.RenameColumn(
                name: "CondtionsArProvider",
                table: "Settings",
                newName: "ConditionsOwnerAr");

            migrationBuilder.RenameColumn(
                name: "CondtionsArClient",
                table: "Settings",
                newName: "ConditionsDeveloperEn");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "Settings",
                newName: "ConditionsDeveloperAr");

            migrationBuilder.RenameColumn(
                name: "AboutUsEnProvider",
                table: "Settings",
                newName: "ConditionsDelegateEn");

            migrationBuilder.RenameColumn(
                name: "AboutUsEnClient",
                table: "Settings",
                newName: "ConditionsDelegateAr");

            migrationBuilder.RenameColumn(
                name: "AboutUsArProvider",
                table: "Settings",
                newName: "ConditionsBrokerEn");

            migrationBuilder.RenameColumn(
                name: "AboutUsArClient",
                table: "Settings",
                newName: "ConditionsBrokerAr");

            migrationBuilder.AddColumn<string>(
                name: "AboutUsAr",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutUsEn",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutUsAr",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AboutUsEn",
                table: "Settings");

            migrationBuilder.RenameColumn(
                name: "UserName_SMS",
                table: "Settings",
                newName: "UserNameSms");

            migrationBuilder.RenameColumn(
                name: "ServerKey_FCM",
                table: "Settings",
                newName: "SenderName");

            migrationBuilder.RenameColumn(
                name: "SenderName_SMS",
                table: "Settings",
                newName: "SenderId");

            migrationBuilder.RenameColumn(
                name: "SenderId_FCM",
                table: "Settings",
                newName: "PasswordSms");

            migrationBuilder.RenameColumn(
                name: "Password_SMS",
                table: "Settings",
                newName: "CondtionsEnProvider");

            migrationBuilder.RenameColumn(
                name: "CondtionsOwnerEn",
                table: "Settings",
                newName: "CondtionsEnClient");

            migrationBuilder.RenameColumn(
                name: "ConditionsOwnerAr",
                table: "Settings",
                newName: "CondtionsArProvider");

            migrationBuilder.RenameColumn(
                name: "ConditionsDeveloperEn",
                table: "Settings",
                newName: "CondtionsArClient");

            migrationBuilder.RenameColumn(
                name: "ConditionsDeveloperAr",
                table: "Settings",
                newName: "ApplicationId");

            migrationBuilder.RenameColumn(
                name: "ConditionsDelegateEn",
                table: "Settings",
                newName: "AboutUsEnProvider");

            migrationBuilder.RenameColumn(
                name: "ConditionsDelegateAr",
                table: "Settings",
                newName: "AboutUsEnClient");

            migrationBuilder.RenameColumn(
                name: "ConditionsBrokerEn",
                table: "Settings",
                newName: "AboutUsArProvider");

            migrationBuilder.RenameColumn(
                name: "ConditionsBrokerAr",
                table: "Settings",
                newName: "AboutUsArClient");
        }
    }
}
