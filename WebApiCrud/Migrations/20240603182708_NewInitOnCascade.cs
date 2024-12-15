using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiCrud.Migrations
{
    /// <inheritdoc />
    public partial class NewInitOnCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f99e235-1d0e-487d-a6d3-cfe2bbd667fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62073d74-c1c7-41b4-966a-5e012dd273fc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "55825431-ecb1-4682-814d-e28a5726e6dd", "5680930c-674e-42c9-828f-2608a5186246", "User", "USER" },
                    { "6e3db2c6-af2e-47f9-9929-af50d1e74e8c", "594553c1-3ec1-4cc4-b101-9b55574072f2", "Admin", "ADMIN" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55825431-ecb1-4682-814d-e28a5726e6dd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6e3db2c6-af2e-47f9-9929-af50d1e74e8c");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f99e235-1d0e-487d-a6d3-cfe2bbd667fc", "21433449-bdb1-4a7c-a7f5-a23b0000d997", "Admin", "ADMIN" },
                    { "62073d74-c1c7-41b4-966a-5e012dd273fc", "13a664c3-6330-4f18-821a-9b0ac86ff3c3", "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderId",
                table: "OrderItems",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
