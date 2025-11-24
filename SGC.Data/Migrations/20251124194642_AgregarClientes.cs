using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SGC.Data.Migrations
{
    /// <inheritdoc />
    public partial class AgregarClientes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Identificacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Clientes",
                columns: new[] { "Id", "Activo", "Apellido", "Direccion", "Email", "FechaRegistro", "Identificacion", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, true, "Mora", "San José, Costa Rica", "carlos.mora@email.com", new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "101110111", "Carlos", "88887777" },
                    { 2, true, "González", "Heredia, Costa Rica", "ana.gonzalez@email.com", new DateTime(2024, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "202220222", "Ana", "77776666" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
