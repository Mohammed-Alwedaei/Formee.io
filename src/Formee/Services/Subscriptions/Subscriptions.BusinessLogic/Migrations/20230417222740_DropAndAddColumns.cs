using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Subscriptions.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class DropAndAddColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasLiveChatAccess",
                table: "SubscriptionFeatures");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfSites",
                table: "SubscriptionFeatures",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfSites",
                table: "SubscriptionFeatures");

            migrationBuilder.AddColumn<bool>(
                name: "HasLiveChatAccess",
                table: "SubscriptionFeatures",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
