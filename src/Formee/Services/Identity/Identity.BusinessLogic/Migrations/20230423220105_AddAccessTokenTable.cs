using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Identity.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class AddAccessTokenTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Avatar_UserId",
                table: "Avatar");

            migrationBuilder.CreateTable(
                name: "AccessToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    IssuedFor = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsExpired = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessToken", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_AvatarId",
                table: "User",
                column: "AvatarId");

            migrationBuilder.CreateIndex(
                name: "IX_Avatar_UserId",
                table: "Avatar",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Avatar_AvatarId",
                table: "User",
                column: "AvatarId",
                principalTable: "Avatar",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Avatar_AvatarId",
                table: "User");

            migrationBuilder.DropTable(
                name: "AccessToken");

            migrationBuilder.DropIndex(
                name: "IX_User_AvatarId",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_Avatar_UserId",
                table: "Avatar");

            migrationBuilder.CreateIndex(
                name: "IX_Avatar_UserId",
                table: "Avatar",
                column: "UserId",
                unique: true);
        }
    }
}
