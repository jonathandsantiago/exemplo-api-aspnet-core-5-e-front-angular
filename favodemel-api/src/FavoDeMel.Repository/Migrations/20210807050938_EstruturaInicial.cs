using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FavoDeMel.Repository.Migrations
{
    public partial class EstruturaInicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    IdProduto = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.IdProduto);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    IdUsuario = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: true),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Perfil = table.Column<int>(type: "integer", nullable: false),
                    Comissao = table.Column<decimal>(type: "numeric", nullable: false),
                    Ativo = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.IdUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Comanda",
                columns: table => new
                {
                    IdComanda = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalAPagar = table.Column<decimal>(type: "numeric", nullable: false),
                    GorjetaGarcom = table.Column<decimal>(type: "numeric", nullable: false),
                    Situacao = table.Column<int>(type: "integer", nullable: false),
                    GarcomId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comanda", x => x.IdComanda);
                    table.ForeignKey(
                        name: "FK_Comanda_Usuario_GarcomId",
                        column: x => x.GarcomId,
                        principalTable: "Usuario",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ComandaPedido",
                columns: table => new
                {
                    IdComandaPedido = table.Column<Guid>(type: "uuid", nullable: false),
                    ComandaId1 = table.Column<Guid>(type: "uuid", nullable: false),
                    ProdutoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantidade = table.Column<int>(type: "integer", nullable: false),
                    Situacao = table.Column<int>(type: "integer", nullable: false),
                    ComandaId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComandaPedido", x => x.IdComandaPedido);
                    table.ForeignKey(
                        name: "FK_ComandaPedido_Comanda_ComandaId",
                        column: x => x.ComandaId,
                        principalTable: "Comanda",
                        principalColumn: "IdComanda",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComandaPedido_Comanda_ComandaId1",
                        column: x => x.ComandaId1,
                        principalTable: "Comanda",
                        principalColumn: "IdComanda",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComandaPedido_Produto_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produto",
                        principalColumn: "IdProduto",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comanda_GarcomId",
                table: "Comanda",
                column: "GarcomId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaPedido_ComandaId",
                table: "ComandaPedido",
                column: "ComandaId");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaPedido_ComandaId1",
                table: "ComandaPedido",
                column: "ComandaId1");

            migrationBuilder.CreateIndex(
                name: "IX_ComandaPedido_ProdutoId",
                table: "ComandaPedido",
                column: "ProdutoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComandaPedido");

            migrationBuilder.DropTable(
                name: "Comanda");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
