using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Analytics.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class AddDomainColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Domain",
                table: "Site",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Domain",
                table: "Site");
        }
    }
}
