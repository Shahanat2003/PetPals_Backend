using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7_petPals.Migrations
{
    /// <inheritdoc />
    public partial class initiall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
