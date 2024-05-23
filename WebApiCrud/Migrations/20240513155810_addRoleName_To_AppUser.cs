using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApiCrud.Migrations
{
    /// <inheritdoc />
    public partial class addRoleName_To_AppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fb04dae-df41-4400-8ed2-05effe321b4d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "80c940e9-43aa-42be-a065-43663de36c9d");

            migrationBuilder.AddColumn<string>(
                name: "roleName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "75babced-be27-412f-bdad-8ea7f6971745", "4420632b-8a8b-4ae6-ac6a-93405f95c61e", "User", "USER" },
                    { "f9893312-0785-4c8b-acbb-761bbce097d4", "ea023f64-fd0c-4f5b-8418-10ee585bad0a", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "75babced-be27-412f-bdad-8ea7f6971745");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f9893312-0785-4c8b-acbb-761bbce097d4");

            migrationBuilder.DropColumn(
                name: "roleName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4fb04dae-df41-4400-8ed2-05effe321b4d", "5a511d46-56bb-4537-a5ed-c8ba3c55d46a", "User", "USER" },
                    { "80c940e9-43aa-42be-a065-43663de36c9d", "c3ffabe4-6056-4f74-854e-145cba4cf9e8", "Admin", "ADMIN" }
                });
        }
    }
}
