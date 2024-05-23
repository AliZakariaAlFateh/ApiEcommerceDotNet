using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCrud.Migrations
{
    /// <inheritdoc />
    public partial class AddimageFOrUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageName",
                table: "AspNetUsers");
        }
    }
}
