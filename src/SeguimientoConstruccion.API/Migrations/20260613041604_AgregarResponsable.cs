using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeguimientoConstruccion.API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarResponsable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResponsableId",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Responsables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsables", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_ResponsableId",
                table: "Tareas",
                column: "ResponsableId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Responsables_ResponsableId",
                table: "Tareas",
                column: "ResponsableId",
                principalTable: "Responsables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Responsables_ResponsableId",
                table: "Tareas");

            migrationBuilder.DropTable(
                name: "Responsables");

            migrationBuilder.DropIndex(
                name: "IX_Tareas_ResponsableId",
                table: "Tareas");

            migrationBuilder.DropColumn(
                name: "ResponsableId",
                table: "Tareas");
        }
    }
}
