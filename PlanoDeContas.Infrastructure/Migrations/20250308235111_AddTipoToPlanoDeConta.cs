using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanoDeContas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoToPlanoDeConta : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "PlanoDeContas",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "PlanoDeContas");
        }
    }
}
