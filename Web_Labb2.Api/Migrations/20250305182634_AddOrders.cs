using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Labb2.Migrations
{
    /// <inheritdoc />
    public partial class AddOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductEntitys_CustomerEntitys_CustomerEntityId",
                table: "ProductEntitys");

            migrationBuilder.DropIndex(
                name: "IX_ProductEntitys_CustomerEntityId",
                table: "ProductEntitys");

            migrationBuilder.DropColumn(
                name: "CustomerEntityId",
                table: "ProductEntitys");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Orders_CustomerEntitys_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "CustomerEntitys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderDetailID = table.Column<int>(type: "int", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderID, x.OrderDetailID });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_ProductEntitys_ProductID",
                        column: x => x.ProductID,
                        principalTable: "ProductEntitys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductID",
                table: "OrderDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                column: "CustomerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "CustomerEntityId",
                table: "ProductEntitys",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntitys_CustomerEntityId",
                table: "ProductEntitys",
                column: "CustomerEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductEntitys_CustomerEntitys_CustomerEntityId",
                table: "ProductEntitys",
                column: "CustomerEntityId",
                principalTable: "CustomerEntitys",
                principalColumn: "Id");
        }
    }
}
