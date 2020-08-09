using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SetProductForSliderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Slider",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Slider_ProductId",
                table: "Slider",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Slider_Product_ProductId",
                table: "Slider",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Slider_Product_ProductId",
                table: "Slider");

            migrationBuilder.DropIndex(
                name: "IX_Slider_ProductId",
                table: "Slider");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Slider");
        }
    }
}
