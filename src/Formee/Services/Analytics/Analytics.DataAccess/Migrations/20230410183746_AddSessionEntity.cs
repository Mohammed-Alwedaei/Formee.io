using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Analytics.BusinessLogic.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageHit_Category_CategoryId",
                table: "PageHit");

            migrationBuilder.DropForeignKey(
                name: "FK_PageHit_Site_SiteId",
                table: "PageHit");

            migrationBuilder.DropForeignKey(
                name: "FK_Site_Category_CategoryId",
                table: "Site");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Site",
                table: "Site");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageHit",
                table: "PageHit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "Site",
                newName: "Sites");

            migrationBuilder.RenameTable(
                name: "PageHit",
                newName: "PageHits");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_Site_CategoryId",
                table: "Sites",
                newName: "IX_Sites_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PageHit_SiteId",
                table: "PageHits",
                newName: "IX_PageHits_SiteId");

            migrationBuilder.RenameIndex(
                name: "IX_PageHit_CategoryId",
                table: "PageHits",
                newName: "IX_PageHits_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sites",
                table: "Sites",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageHits",
                table: "PageHits",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastDeviceHit = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_PageHits_Categories_CategoryId",
                table: "PageHits",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageHits_Sites_SiteId",
                table: "PageHits",
                column: "SiteId",
                principalTable: "Sites",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sites_Categories_CategoryId",
                table: "Sites",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageHits_Categories_CategoryId",
                table: "PageHits");

            migrationBuilder.DropForeignKey(
                name: "FK_PageHits_Sites_SiteId",
                table: "PageHits");

            migrationBuilder.DropForeignKey(
                name: "FK_Sites_Categories_CategoryId",
                table: "Sites");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sites",
                table: "Sites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PageHits",
                table: "PageHits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "Sites",
                newName: "Site");

            migrationBuilder.RenameTable(
                name: "PageHits",
                newName: "PageHit");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_Sites_CategoryId",
                table: "Site",
                newName: "IX_Site_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_PageHits_SiteId",
                table: "PageHit",
                newName: "IX_PageHit_SiteId");

            migrationBuilder.RenameIndex(
                name: "IX_PageHits_CategoryId",
                table: "PageHit",
                newName: "IX_PageHit_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Site",
                table: "Site",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PageHit",
                table: "PageHit",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageHit_Category_CategoryId",
                table: "PageHit",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageHit_Site_SiteId",
                table: "PageHit",
                column: "SiteId",
                principalTable: "Site",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Site_Category_CategoryId",
                table: "Site",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id");
        }
    }
}
