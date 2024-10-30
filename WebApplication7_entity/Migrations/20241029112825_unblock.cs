using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7_petPals.Migrations
{
    /// <inheritdoc />
    public partial class unblock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "blocked",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Order_id",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "blocked",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "Order_id",
                table: "orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
