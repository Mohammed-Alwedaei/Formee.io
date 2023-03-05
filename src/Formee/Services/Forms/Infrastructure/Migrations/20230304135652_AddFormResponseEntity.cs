using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddFormResponseEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Forms_FormId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Forms_Details_DetailsId",
                table: "Forms");

            migrationBuilder.DropTable(
                name: "FieldsWarehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forms",
                table: "Forms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Fields",
                table: "Fields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Details",
                table: "Details");

            migrationBuilder.RenameTable(
                name: "Forms",
                newName: "Form");

            migrationBuilder.RenameTable(
                name: "Fields",
                newName: "Field");

            migrationBuilder.RenameTable(
                name: "Details",
                newName: "Detail");

            migrationBuilder.RenameIndex(
                name: "IX_Forms_DetailsId",
                table: "Form",
                newName: "IX_Form_DetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Fields_FormId",
                table: "Field",
                newName: "IX_Field_FormId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Form",
                table: "Form",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Field",
                table: "Field",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Detail",
                table: "Detail",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FormResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormResponse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FieldsResponseField",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FormResponseEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldsResponseField", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldsResponseField_FormResponse_FormResponseEntityId",
                        column: x => x.FormResponseEntityId,
                        principalTable: "FormResponse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_FieldsResponseField_FormResponseEntityId",
                table: "FieldsResponseField",
                column: "FormResponseEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Field_Form_FormId",
                table: "Field",
                column: "FormId",
                principalTable: "Form",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Form_Detail_DetailsId",
                table: "Form",
                column: "DetailsId",
                principalTable: "Detail",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Field_Form_FormId",
                table: "Field");

            migrationBuilder.DropForeignKey(
                name: "FK_Form_Detail_DetailsId",
                table: "Form");

            migrationBuilder.DropTable(
                name: "FieldsResponseField");

            migrationBuilder.DropTable(
                name: "FormResponse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Form",
                table: "Form");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Field",
                table: "Field");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Detail",
                table: "Detail");

            migrationBuilder.RenameTable(
                name: "Form",
                newName: "Forms");

            migrationBuilder.RenameTable(
                name: "Field",
                newName: "Fields");

            migrationBuilder.RenameTable(
                name: "Detail",
                newName: "Details");

            migrationBuilder.RenameIndex(
                name: "IX_Form_DetailsId",
                table: "Forms",
                newName: "IX_Forms_DetailsId");

            migrationBuilder.RenameIndex(
                name: "IX_Field_FormId",
                table: "Fields",
                newName: "IX_Fields_FormId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forms",
                table: "Forms",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Fields",
                table: "Fields",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Details",
                table: "Details",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "FieldsWarehouse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldsWarehouse", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Forms_FormId",
                table: "Fields",
                column: "FormId",
                principalTable: "Forms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Forms_Details_DetailsId",
                table: "Forms",
                column: "DetailsId",
                principalTable: "Details",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
