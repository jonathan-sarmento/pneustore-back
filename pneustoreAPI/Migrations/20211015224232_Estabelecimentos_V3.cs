using Microsoft.EntityFrameworkCore.Migrations;

namespace pneustoreAPI.Migrations
{
    public partial class Estabelecimentos_V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Estabelecimentoid",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_Estabelecimentoid",
                table: "Product",
                column: "Estabelecimentoid");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Estabelecimentos_Estabelecimentoid",
                table: "Product",
                column: "Estabelecimentoid",
                principalTable: "Estabelecimentos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Estabelecimentos_Estabelecimentoid",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_Estabelecimentoid",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Estabelecimentoid",
                table: "Product");
        }
    }
}
