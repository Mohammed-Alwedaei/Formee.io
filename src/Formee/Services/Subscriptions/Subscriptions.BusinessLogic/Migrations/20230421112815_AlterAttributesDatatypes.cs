using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscriptions.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class AlterAttributesDatatypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subscriptions",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Subscriptions",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminEmail",
                table: "Subscriptions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminEmail",
                table: "Subscriptions");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Subscriptions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
