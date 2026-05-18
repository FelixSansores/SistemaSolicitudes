using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGS.Class.Migrations
{
    /// <inheritdoc />
    public partial class AddCreateByUserToRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_CreatedByUserId",
                table: "Requests",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_CreatedByUserId",
                table: "Requests",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_CreatedByUserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_CreatedByUserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Requests");
        }
    }
}
