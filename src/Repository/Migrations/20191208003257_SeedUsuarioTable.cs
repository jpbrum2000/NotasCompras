using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class SeedUsuarioTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Papel",
                table: "Usuario",
                nullable: false,
                defaultValue: 0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Papel",
                table: "Usuario");
        }
    }
}
