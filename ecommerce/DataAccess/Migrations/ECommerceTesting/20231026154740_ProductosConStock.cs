using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations.ECommerceTesting
{
    public partial class ProductosConStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ColorProducto");

            migrationBuilder.AddColumn<bool>(
                name: "AplicaParaPromociones",
                table: "Productos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "Productos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Productos_ColorId",
                table: "Productos",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_Colores_ColorId",
                table: "Productos",
                column: "ColorId",
                principalTable: "Colores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_Colores_ColorId",
                table: "Productos");

            migrationBuilder.DropIndex(
                name: "IX_Productos_ColorId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "AplicaParaPromociones",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Productos");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "Productos");

            migrationBuilder.CreateTable(
                name: "ColorProducto",
                columns: table => new
                {
                    ColoresId = table.Column<int>(type: "int", nullable: false),
                    ProductosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColorProducto", x => new { x.ColoresId, x.ProductosId });
                    table.ForeignKey(
                        name: "FK_ColorProducto_Colores_ColoresId",
                        column: x => x.ColoresId,
                        principalTable: "Colores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ColorProducto_Productos_ProductosId",
                        column: x => x.ProductosId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ColorProducto_ProductosId",
                table: "ColorProducto",
                column: "ProductosId");
        }
    }
}
