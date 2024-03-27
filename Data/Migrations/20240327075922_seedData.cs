using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Avatar", "Email", "FirstName", "LastName", "Password", "PhoneNumber", "UserName" },
                values: new object[,]
                {
                    { 1, null, "fszymanowski0@com.com", "Fawnia", "Szymanowski", "74a14ea74c47ecdf30f940974dc9dc20", "0335487991", "fszymanowski0" },
                    { 2, null, "apeacocke1@google.ca", "Fawnia", "Alexandros", "c52c635d98738ff357b13d9e4368aff6", "0354579415", "apeacocke1" },
                    { 3, null, "cpancoast2@wsj.com", "Cazzie", "Pancoast", "7f45928bce3ba52d77dee0cf1a8bbfdf", "0354596415", "cpancoast2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
