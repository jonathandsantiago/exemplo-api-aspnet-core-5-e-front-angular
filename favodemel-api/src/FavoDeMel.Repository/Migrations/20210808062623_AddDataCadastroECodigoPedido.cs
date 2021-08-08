using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FavoDeMel.Repository.Migrations
{
    public partial class AddDataCadastroECodigoPedido : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Codigo",
                table: "Comanda",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DataCadastro",
                table: "Comanda",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Codigo",
                table: "Comanda");

            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Comanda");
        }
    }
}
