using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class AddPendingMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Avatar_AvatarId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_AvatarId",
                table: "User");

            migrationBuilder.CreateIndex(
                name: "IX_Avatar_UserId",
                table: "Avatar",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Avatar_User_UserId",
                table: "Avatar",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avatar_User_UserId",
                table: "Avatar");

            migrationBuilder.DropIndex(
                name: "IX_Avatar_UserId",
                table: "Avatar");

            migrationBuilder.CreateIndex(
                name: "IX_User_AvatarId",
                table: "User",
                column: "AvatarId",
                unique: true,
                filter: "[AvatarId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Avatar_AvatarId",
                table: "User",
                column: "AvatarId",
                principalTable: "Avatar",
                principalColumn: "Id");
        }
    }
}
