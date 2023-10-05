using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class ActualizarProductoConCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Compras_CompraId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_CompraId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "CompraId",
                table: "Productos");

            migrationBuilder.CreateTable(
                name: "CompraProducto",
                columns: table => new
                {
                    ComprasId = table.Column<int>(type: "int", nullable: false),
                    ProductosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraProducto", x => new { x.ComprasId, x.ProductosId });
                    table.ForeignKey(
                        name: "FK_CompraProducto_Compras_ComprasId",
                        column: x => x.ComprasId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraProducto_Productos_ProductosId",
                        column: x => x.ProductosId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompraProducto_ProductosId",
                table: "CompraProducto",
                column: "ProductosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompraProducto");

            migrationBuilder.AddColumn<int>(
                name: "CompraId",
                table: "Productos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CompraId",
                table: "Productos",
                column: "CompraId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Compras_CompraId",
                table: "Productos",
                column: "CompraId",
                principalTable: "Compras",
                principalColumn: "Id");
        }
    }
}
