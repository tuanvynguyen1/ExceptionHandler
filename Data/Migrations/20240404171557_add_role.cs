using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class add_role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JWT_Users_UsersId",
                table: "JWT");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "JWT",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_JWT_UsersId",
                table: "JWT",
                newName: "IX_JWT_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JWT_Users_UserId",
                table: "JWT",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JWT_Users_UserId",
                table: "JWT");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "JWT",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_JWT_UserId",
                table: "JWT",
                newName: "IX_JWT_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_JWT_Users_UsersId",
                table: "JWT",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
