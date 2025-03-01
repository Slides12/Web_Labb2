using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Labb2.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdressEntitys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdressEntitys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomerEntitys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddressInformationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerEntitys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerEntitys_AdressEntitys_AddressInformationId",
                        column: x => x.AddressInformationId,
                        principalTable: "AdressEntitys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProductEntitys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    CustomerEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEntitys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductEntitys_CustomerEntitys_CustomerEntityId",
                        column: x => x.CustomerEntityId,
                        principalTable: "CustomerEntitys",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerEntitys_AddressInformationId",
                table: "CustomerEntitys",
                column: "AddressInformationId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEntitys_CustomerEntityId",
                table: "ProductEntitys",
                column: "CustomerEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductEntitys");

            migrationBuilder.DropTable(
                name: "CustomerEntitys");

            migrationBuilder.DropTable(
                name: "AdressEntitys");
        }
    }
}
