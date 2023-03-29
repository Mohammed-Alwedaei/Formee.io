using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscriptions.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnToCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Coupons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Coupons");
        }
    }
}
