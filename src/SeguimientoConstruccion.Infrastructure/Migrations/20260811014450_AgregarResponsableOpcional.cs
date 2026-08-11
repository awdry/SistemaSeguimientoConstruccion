using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeguimientoConstruccion.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarResponsableOpcional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Responsables_ResponsableId",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "ResponsableId",
                table: "Tareas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Responsables_ResponsableId",
                table: "Tareas",
                column: "ResponsableId",
                principalTable: "Responsables",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tareas_Responsables_ResponsableId",
                table: "Tareas");

            migrationBuilder.AlterColumn<int>(
                name: "ResponsableId",
                table: "Tareas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tareas_Responsables_ResponsableId",
                table: "Tareas",
                column: "ResponsableId",
                principalTable: "Responsables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
