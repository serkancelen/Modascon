using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Repositories.Migrations
{
    /// <inheritdoc />
    public partial class admin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1caade03-d77d-4227-bd2b-ed420bb053d2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f64ab57-22a7-420a-bdfc-ab938ebe58f0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac558705-9ad6-4fe2-93c0-39c8364ae396");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "87204ac4-e1b7-4f49-87ef-c535d313c68e", null, "Admin", "ADMIN" },
                    { "9521c769-72b8-4e11-8db0-3ce1dbc17b23", null, "Editör", "EDITOR" },
                    { "fb0feb64-9967-4e5f-ba66-4a68019f412c", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87204ac4-e1b7-4f49-87ef-c535d313c68e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9521c769-72b8-4e11-8db0-3ce1dbc17b23");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb0feb64-9967-4e5f-ba66-4a68019f412c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1caade03-d77d-4227-bd2b-ed420bb053d2", null, "User", "USER" },
                    { "6f64ab57-22a7-420a-bdfc-ab938ebe58f0", null, "Editör", "EDITOR" },
                    { "ac558705-9ad6-4fe2-93c0-39c8364ae396", null, "Admin", "ADMIN" }
                });
        }
    }
}
