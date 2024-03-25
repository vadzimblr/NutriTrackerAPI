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
                values: new object[] { "1493220f-d8bf-4176-a4c6-72b16521ca8d", null, "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "77b6f5b4-9993-446e-b952-13624a256293", null, "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1493220f-d8bf-4176-a4c6-72b16521ca8d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "77b6f5b4-9993-446e-b952-13624a256293");

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
