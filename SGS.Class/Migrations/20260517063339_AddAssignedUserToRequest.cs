using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGS.Class.Migrations
{
    /// <inheritdoc />
    public partial class AddAssignedUserToRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AssignedUserId",
                table: "Requests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_AssignedUserId",
                table: "Requests",
                column: "AssignedUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AspNetUsers_AssignedUserId",
                table: "Requests",
                column: "AssignedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AspNetUsers_AssignedUserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_AssignedUserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "AssignedUserId",
                table: "Requests");
        }
    }
}
