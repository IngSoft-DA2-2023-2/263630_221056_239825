using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class agregoFechaCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Usuarios_UsuarioId",
                table: "Compras");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Compras",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCompra",
                table: "Compras",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "NombrePromo",
                table: "Compras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Precio",
                table: "Compras",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Usuarios_UsuarioId",
                table: "Compras",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compras_Usuarios_UsuarioId",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "FechaCompra",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "NombrePromo",
                table: "Compras");

            migrationBuilder.DropColumn(
                name: "Precio",
                table: "Compras");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Compras",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Compras_Usuarios_UsuarioId",
                table: "Compras",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
