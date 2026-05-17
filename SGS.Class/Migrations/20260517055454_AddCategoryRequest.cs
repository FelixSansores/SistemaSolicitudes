using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGS.Class.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CategoryId",
                table: "Requests",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Categories_CategoryId",
                table: "Requests",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Categories_CategoryId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CategoryId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Requests");
        }
    }
}
