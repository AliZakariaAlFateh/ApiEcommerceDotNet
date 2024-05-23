using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApiCrud.Migrations
{
    /// <inheritdoc />
    public partial class addimagenamecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageName",
                table: "Products");
        }
    }
}
