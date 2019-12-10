using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfFaixaVistAprov",
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
                    table.PrimaryKey("PK_ConfFaixaVistAprov", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotasCompra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DataEmissao = table.Column<DateTime>(nullable: false),
                    ValorMercadorias = table.Column<double>(nullable: false),
                    ValorDesconto = table.Column<double>(nullable: false),
                    ValorFrete = table.Column<double>(nullable: false),
                    ValorTotal = table.Column<double>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasCompra", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(nullable: true),
                    Senha = table.Column<string>(nullable: true),
                    Papel = table.Column<int>(nullable: false),
                    ValorMinimo = table.Column<double>(nullable: false),
                    ValorMaximo = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistAprovNotaCompra",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Data = table.Column<DateTime>(nullable: false),
                    UsuarioId = table.Column<int>(nullable: false),
                    Operacao = table.Column<int>(nullable: false),
                    NotaCompraId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistAprovNotaCompra", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistAprovNotaCompra_NotasCompra_NotaCompraId",
                        column: x => x.NotaCompraId,
                        principalTable: "NotasCompra",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistAprovNotaCompra_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ConfFaixaVistAprov",
                columns: new[] { "Id", "Aprovacoes", "FaixaMax", "FaixaMin", "Vistos" },
                values: new object[] { 1, 0, 1000.0, 0.0, 1 });

            migrationBuilder.InsertData(
                table: "ConfFaixaVistAprov",
                columns: new[] { "Id", "Aprovacoes", "FaixaMax", "FaixaMin", "Vistos" },
                values: new object[] { 2, 1, 10000.0, 1000.01, 1 });

            migrationBuilder.InsertData(
                table: "ConfFaixaVistAprov",
                columns: new[] { "Id", "Aprovacoes", "FaixaMax", "FaixaMin", "Vistos" },
                values: new object[] { 3, 1, 50000.0, 10000.01, 2 });

            migrationBuilder.InsertData(
                table: "ConfFaixaVistAprov",
                columns: new[] { "Id", "Aprovacoes", "FaixaMax", "FaixaMin", "Vistos" },
                values: new object[] { 4, 2, 999999.98999999999, 50000.010000000002, 2 });

            migrationBuilder.InsertData(
                table: "NotasCompra",
                columns: new[] { "Id", "DataEmissao", "Status", "ValorDesconto", "ValorFrete", "ValorMercadorias", "ValorTotal" },
                values: new object[] { 1, new DateTime(2019, 12, 1, 8, 30, 0, 0, DateTimeKind.Unspecified), 0, 10.0, 15.0, 100.09999999999999, 105.09999999999999 });

            migrationBuilder.InsertData(
                table: "NotasCompra",
                columns: new[] { "Id", "DataEmissao", "Status", "ValorDesconto", "ValorFrete", "ValorMercadorias", "ValorTotal" },
                values: new object[] { 2, new DateTime(2019, 12, 2, 18, 30, 0, 0, DateTimeKind.Unspecified), 0, 10.0, 15.0, 1500.0999999999999, 1505.0999999999999 });

            migrationBuilder.InsertData(
                table: "NotasCompra",
                columns: new[] { "Id", "DataEmissao", "Status", "ValorDesconto", "ValorFrete", "ValorMercadorias", "ValorTotal" },
                values: new object[] { 3, new DateTime(2019, 12, 20, 8, 30, 0, 0, DateTimeKind.Unspecified), 0, 10.0, 15.0, 10500.1, 10505.1 });

            migrationBuilder.InsertData(
                table: "NotasCompra",
                columns: new[] { "Id", "DataEmissao", "Status", "ValorDesconto", "ValorFrete", "ValorMercadorias", "ValorTotal" },
                values: new object[] { 4, new DateTime(2019, 12, 25, 18, 30, 0, 0, DateTimeKind.Unspecified), 0, 10.0, 15.0, 200000.0, 200005.0 });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Login", "Papel", "Senha", "ValorMaximo", "ValorMinimo" },
                values: new object[] { 1, "gerente", 1, "gerente", 999999.98999999999, 50000.010000000002 });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Login", "Papel", "Senha", "ValorMaximo", "ValorMinimo" },
                values: new object[] { 2, "subgerente", 1, "subgerente", 999999.98999999999, 1000.01 });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Login", "Papel", "Senha", "ValorMaximo", "ValorMinimo" },
                values: new object[] { 3, "vendedor", 0, "vendedor", 999999.98999999999, 10000.01 });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "Login", "Papel", "Senha", "ValorMaximo", "ValorMinimo" },
                values: new object[] { 4, "auxiliar", 0, "auxiliar", 999999.98999999999, 0.0 });

            migrationBuilder.CreateIndex(
                name: "IX_HistAprovNotaCompra_NotaCompraId",
                table: "HistAprovNotaCompra",
                column: "NotaCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_HistAprovNotaCompra_UsuarioId",
                table: "HistAprovNotaCompra",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfFaixaVistAprov");

            migrationBuilder.DropTable(
                name: "HistAprovNotaCompra");

            migrationBuilder.DropTable(
                name: "NotasCompra");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
