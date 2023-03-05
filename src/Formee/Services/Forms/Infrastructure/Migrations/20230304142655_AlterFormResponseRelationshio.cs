using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlterFormResponseRelationshio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldsResponseField_FormResponse_FormResponseEntityId",
                table: "FieldsResponseField");

            migrationBuilder.DropIndex(
                name: "IX_FieldsResponseField_FormResponseEntityId",
                table: "FieldsResponseField");

            migrationBuilder.DropColumn(
                name: "FormResponseEntityId",
                table: "FieldsResponseField");

            migrationBuilder.CreateIndex(
                name: "IX_FieldsResponseField_FormResponseId",
                table: "FieldsResponseField",
                column: "FormResponseId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsResponseField_FormResponse_FormResponseId",
                table: "FieldsResponseField",
                column: "FormResponseId",
                principalTable: "FormResponse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FieldsResponseField_FormResponse_FormResponseId",
                table: "FieldsResponseField");

            migrationBuilder.DropIndex(
                name: "IX_FieldsResponseField_FormResponseId",
                table: "FieldsResponseField");

            migrationBuilder.AddColumn<int>(
                name: "FormResponseEntityId",
                table: "FieldsResponseField",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FieldsResponseField_FormResponseEntityId",
                table: "FieldsResponseField",
                column: "FormResponseEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_FieldsResponseField_FormResponse_FormResponseEntityId",
                table: "FieldsResponseField",
                column: "FormResponseEntityId",
                principalTable: "FormResponse",
                principalColumn: "Id");
        }
    }
}
