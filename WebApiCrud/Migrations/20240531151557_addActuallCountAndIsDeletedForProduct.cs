using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiCrud.Migrations
{
    /// <inheritdoc />
    public partial class addActuallCountAndIsDeletedForProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "751e7131-443f-436f-88a8-6ec680e1a7ae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8271d4a-f488-4878-bbc3-b1e37262844a");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ActualCount",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "f1618952-02ad-4837-96bb-14cdac3a4e41", "7580361a-f5c2-4171-9bdc-1ae6ae8cb5df", "User", "USER" },
                    { "f5af63c5-78a8-4e45-b88b-2080fcff2f12", "cb5a2360-93aa-4487-881b-c2d58850aeea", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f1618952-02ad-4837-96bb-14cdac3a4e41");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5af63c5-78a8-4e45-b88b-2080fcff2f12");

            migrationBuilder.AlterColumn<int>(
                name: "isdeleted",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ActualCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "751e7131-443f-436f-88a8-6ec680e1a7ae", "7b907dec-a292-4edd-8b2b-9348b55f5b17", "User", "USER" },
                    { "a8271d4a-f488-4878-bbc3-b1e37262844a", "1a2f314e-1e45-40f4-9cf9-8867bae4a784", "Admin", "ADMIN" }
                });
        }
    }
}
