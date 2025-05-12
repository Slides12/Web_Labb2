using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Labb2.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomerId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "UserEntitys",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserEntitys_CustomerId",
                table: "UserEntitys",
                column: "CustomerId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserEntitys_CustomerEntitys_CustomerId",
                table: "UserEntitys",
                column: "CustomerId",
                principalTable: "CustomerEntitys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserEntitys_CustomerEntitys_CustomerId",
                table: "UserEntitys");

            migrationBuilder.DropIndex(
                name: "IX_UserEntitys_CustomerId",
                table: "UserEntitys");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "UserEntitys");
        }
    }
}
