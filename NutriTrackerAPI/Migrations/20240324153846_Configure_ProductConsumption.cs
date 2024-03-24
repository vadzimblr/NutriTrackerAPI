using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriTrackerAPI.Migrations
{
    public partial class Configure_ProductConsumption : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductConsumptions_Products_ConsumedProductId",
                table: "ProductConsumptions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "47764a02-0205-4f83-806b-6cf3849b2940");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4378224-c2e8-4dec-9a3e-0c9e5c8508c7");

            migrationBuilder.AlterColumn<Guid>(
                name: "ConsumedProductId",
                table: "ProductConsumptions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "899e3d55-9dd8-414e-b068-00e349ed2781", null, "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f89acdb6-5798-46da-9e85-1a280a5bfa63", null, "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductConsumptions_Products_ConsumedProductId",
                table: "ProductConsumptions",
                column: "ConsumedProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductConsumptions_Products_ConsumedProductId",
                table: "ProductConsumptions");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "899e3d55-9dd8-414e-b068-00e349ed2781");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f89acdb6-5798-46da-9e85-1a280a5bfa63");

            migrationBuilder.AlterColumn<Guid>(
                name: "ConsumedProductId",
                table: "ProductConsumptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "47764a02-0205-4f83-806b-6cf3849b2940", null, "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b4378224-c2e8-4dec-9a3e-0c9e5c8508c7", null, "Admin", "ADMIN" });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductConsumptions_Products_ConsumedProductId",
                table: "ProductConsumptions",
                column: "ConsumedProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
