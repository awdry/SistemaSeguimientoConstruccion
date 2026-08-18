using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeguimientoConstruccion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarProvinciaYDireccion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ubicacion",
                table: "Obras",
                newName: "Provincia");

            migrationBuilder.AddColumn<string>(
                name: "Direccion",
                table: "Obras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Direccion",
                table: "Obras");

            migrationBuilder.RenameColumn(
                name: "Provincia",
                table: "Obras",
                newName: "Ubicacion");
        }
    }
}
