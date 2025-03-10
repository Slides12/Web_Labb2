using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Labb2.Migrations
{
    /// <inheritdoc />
    public partial class AddIMGPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ProductEntitys",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ProductEntitys");
        }
    }
}
