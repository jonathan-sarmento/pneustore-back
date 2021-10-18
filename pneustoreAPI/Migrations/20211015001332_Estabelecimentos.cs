using Microsoft.EntityFrameworkCore.Migrations;

namespace pneustoreAPI.Migrations
{
    public partial class Estabelecimentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estabelecimentos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    distancia = table.Column<double>(type: "float", nullable: false),
                    imagemUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estabelecimentos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EstabPneu",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    EstabelecimentoId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstabPneu", x => new { x.ProductId, x.EstabelecimentoId });
                    table.ForeignKey(
                        name: "FK_EstabPneu_Estabelecimentos_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "Estabelecimentos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstabPneu_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstabPneu_EstabelecimentoId",
                table: "EstabPneu",
                column: "EstabelecimentoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EstabPneu");

            migrationBuilder.DropTable(
                name: "Estabelecimentos");
        }
    }
}
