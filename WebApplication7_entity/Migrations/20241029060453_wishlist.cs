using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7_petPals.Migrations
{
    /// <inheritdoc />
    public partial class wishlist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "wishlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_id = table.Column<int>(type: "int", nullable: false),
                    Product_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_wishlists_Users_User_id",
                        column: x => x.User_id,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_wishlists_products_Product_id",
                        column: x => x.Product_id,
                        principalTable: "products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_wishlists_Product_id",
                table: "wishlists",
                column: "Product_id");

            migrationBuilder.CreateIndex(
                name: "IX_wishlists_User_id",
                table: "wishlists",
                column: "User_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "wishlists");
        }
    }
}
