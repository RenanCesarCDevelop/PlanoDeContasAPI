using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanoDeContas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlanoDeContas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AceitaLancamentos = table.Column<bool>(type: "bit", nullable: false),
                    PaiId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanoDeContas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanoDeContas_PlanoDeContas_PaiId",
                        column: x => x.PaiId,
                        principalTable: "PlanoDeContas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanoDeContas_Codigo",
                table: "PlanoDeContas",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanoDeContas_PaiId",
                table: "PlanoDeContas",
                column: "PaiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanoDeContas");
        }
    }
}
