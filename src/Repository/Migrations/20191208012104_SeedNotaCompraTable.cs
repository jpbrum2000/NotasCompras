using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class SeedNotaCompraTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "NotasCompra",
                nullable: false,
                defaultValue: 0);

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotasCompra",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "NotasCompra",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "NotasCompra",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "NotasCompra",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Status",
                table: "NotasCompra");
        }
    }
}
