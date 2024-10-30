using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication7_petPals.Migrations
{
    /// <inheritdoc />
    public partial class orders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_orders_OrderId",
                table: "orderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_products_ProductId",
                table: "orderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_Users_UserId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_UserId",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orderItems_OrderId",
                table: "orderItems");

            migrationBuilder.DropIndex(
                name: "IX_orderItems_ProductId",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "orderItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "orderItems");

            migrationBuilder.CreateIndex(
                name: "IX_orders_User_id",
                table: "orders",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_Order_id",
                table: "orderItems",
                column: "Order_id");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_Product_id",
                table: "orderItems",
                column: "Product_id");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_orders_Order_id",
                table: "orderItems",
                column: "Order_id",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_products_Product_id",
                table: "orderItems",
                column: "Product_id",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_Users_User_id",
                table: "orders",
                column: "User_id",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_orders_Order_id",
                table: "orderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_orderItems_products_Product_id",
                table: "orderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_Users_User_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orders_User_id",
                table: "orders");

            migrationBuilder.DropIndex(
                name: "IX_orderItems_Order_id",
                table: "orderItems");

            migrationBuilder.DropIndex(
                name: "IX_orderItems_Product_id",
                table: "orderItems");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "orderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "orderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_orders_UserId",
                table: "orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_OrderId",
                table: "orderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_ProductId",
                table: "orderItems",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_orders_OrderId",
                table: "orderItems",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orderItems_products_ProductId",
                table: "orderItems",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_Users_UserId",
                table: "orders",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
