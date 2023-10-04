using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddedEnumConversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Administradores_AdministradorId",
                table: "Compras");

            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Clientes_ClienteId",
                table: "Compras");

            migrationBuilder.DropTable(
                name: "Administradores");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropIndex(
                name: "IX_Compras_AdministradorId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "AdministradorId",
                table: "Compras");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Compras",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_ClienteId",
                table: "Compras",
                newName: "IX_Compras_UsuarioId");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionEntrega = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Usuarios_UsuarioId",
                table: "Compras",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Usuarios_UsuarioId",
                table: "Compras");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Compras",
                newName: "ClienteId");

            migrationBuilder.RenameIndex(
                name: "IX_Compras_UsuarioId",
                table: "Compras",
                newName: "IX_Compras_ClienteId");

            migrationBuilder.AddColumn<int>(
                name: "AdministradorId",
                table: "Compras",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Administradores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionEntrega = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administradores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorreoElectronico = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DireccionEntrega = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Compras_AdministradorId",
                table: "Compras",
                column: "AdministradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Administradores_AdministradorId",
                table: "Compras",
                column: "AdministradorId",
                principalTable: "Administradores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Clientes_ClienteId",
                table: "Compras",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }
    }
}
