using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscriptions.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminEmailColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Coupons");

            migrationBuilder.AddColumn<string>(
                name: "AdminEmail",
                table: "Coupons",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminEmail",
                table: "Coupons");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Coupons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
