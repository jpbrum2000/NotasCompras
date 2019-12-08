using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class SeedConfFaixaVistoAprov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfiguracaoFaixaVistosAprovacoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FaixaMin = table.Column<double>(nullable: false),
                    FaixaMax = table.Column<double>(nullable: false),
                    Vistos = table.Column<int>(nullable: false),
                    Aprovacoes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfiguracaoFaixaVistosAprovacoes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ConfiguracaoFaixaVistosAprovacoes",
                columns: new[] { "Id", "Aprovacoes", "FaixaMax", "FaixaMin", "Vistos" },
                values: new object[] { 1, 0, 1000.0, 0.0, 1 });

            migrationBuilder.InsertData(
                table: "ConfiguracaoFaixaVistosAprovacoes",
                columns: new[] { "Id", "Aprovacoes", "FaixaMax", "FaixaMin", "Vistos" },
                values: new object[] { 2, 1, 10000.0, 1000.01, 1 });

            migrationBuilder.InsertData(
                table: "ConfiguracaoFaixaVistosAprovacoes",
                columns: new[] { "Id", "Aprovacoes", "FaixaMax", "FaixaMin", "Vistos" },
                values: new object[] { 3, 1, 50000.0, 10000.01, 2 });

            migrationBuilder.InsertData(
                table: "ConfiguracaoFaixaVistosAprovacoes",
                columns: new[] { "Id", "Aprovacoes", "FaixaMax", "FaixaMin", "Vistos" },
                values: new object[] { 4, 2, 999999.98999999999, 50000.010000000002, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfiguracaoFaixaVistosAprovacoes");
        }
    }
}
