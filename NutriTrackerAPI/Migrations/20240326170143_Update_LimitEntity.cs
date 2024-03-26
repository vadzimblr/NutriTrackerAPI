using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriTrackerAPI.Migrations
{
    public partial class Update_LimitEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "899e3d55-9dd8-414e-b068-00e349ed2781");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f89acdb6-5798-46da-9e85-1a280a5bfa63");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Limits");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1294b2e0-f589-4a36-b18e-b18480ab1a89", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "86b26379-17d1-4e5b-9727-f708e10fe4cf", null, "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1294b2e0-f589-4a36-b18e-b18480ab1a89");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86b26379-17d1-4e5b-9727-f708e10fe4cf");

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "Limits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "899e3d55-9dd8-414e-b068-00e349ed2781", null, "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f89acdb6-5798-46da-9e85-1a280a5bfa63", null, "Admin", "ADMIN" });
        }
    }
}
