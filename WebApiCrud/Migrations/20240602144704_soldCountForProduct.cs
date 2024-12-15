using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiCrud.Migrations
{
    /// <inheritdoc />
    public partial class soldCountForProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1618952-02ad-4837-96bb-14cdac3a4e41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5af63c5-78a8-4e45-b88b-2080fcff2f12");

            migrationBuilder.AddColumn<int>(
                name: "soldCount",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4f99e235-1d0e-487d-a6d3-cfe2bbd667fc", "21433449-bdb1-4a7c-a7f5-a23b0000d997", "Admin", "ADMIN" },
                    { "62073d74-c1c7-41b4-966a-5e012dd273fc", "13a664c3-6330-4f18-821a-9b0ac86ff3c3", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4f99e235-1d0e-487d-a6d3-cfe2bbd667fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62073d74-c1c7-41b4-966a-5e012dd273fc");

            migrationBuilder.DropColumn(
                name: "soldCount",
                table: "Products");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f1618952-02ad-4837-96bb-14cdac3a4e41", "7580361a-f5c2-4171-9bdc-1ae6ae8cb5df", "User", "USER" },
                    { "f5af63c5-78a8-4e45-b88b-2080fcff2f12", "cb5a2360-93aa-4487-881b-c2d58850aeea", "Admin", "ADMIN" }
                });
        }
    }
}
