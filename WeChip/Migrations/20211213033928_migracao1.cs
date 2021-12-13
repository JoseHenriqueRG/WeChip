using Microsoft.EntityFrameworkCore.Migrations;

namespace WeChip.Migrations
{
    public partial class migracao1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    CodigoStatus = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    FinalizaCliente = table.Column<bool>(type: "INTEGER", nullable: false),
                    ContabilizaVenda = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.CodigoStatus);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Cpf = table.Column<string>(type: "TEXT", nullable: false),
                    Credito = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Telefone = table.Column<string>(type: "TEXT", nullable: false),
                    StatusCodigoStatus = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cliente_Status_StatusCodigoStatus",
                        column: x => x.StatusCodigoStatus,
                        principalTable: "Status",
                        principalColumn: "CodigoStatus",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Oferta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClienteID = table.Column<int>(type: "INTEGER", nullable: true),
                    Cep = table.Column<string>(type: "TEXT", nullable: true),
                    Rua = table.Column<string>(type: "TEXT", nullable: true),
                    Numero = table.Column<int>(type: "INTEGER", nullable: true),
                    Complemento = table.Column<string>(type: "TEXT", nullable: true),
                    Bairro = table.Column<string>(type: "TEXT", nullable: true),
                    Cidade = table.Column<string>(type: "TEXT", nullable: true),
                    Estado = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oferta", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Oferta_Cliente_ClienteID",
                        column: x => x.ClienteID,
                        principalTable: "Cliente",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    CodigoProduto = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Descricao = table.Column<string>(type: "TEXT", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tipo = table.Column<string>(type: "TEXT", nullable: true),
                    OfertaID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.CodigoProduto);
                    table.ForeignKey(
                        name: "FK_Produto_Oferta_OfertaID",
                        column: x => x.OfertaID,
                        principalTable: "Oferta",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_StatusCodigoStatus",
                table: "Cliente",
                column: "StatusCodigoStatus");

            migrationBuilder.CreateIndex(
                name: "IX_Oferta_ClienteID",
                table: "Oferta",
                column: "ClienteID");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_OfertaID",
                table: "Produto",
                column: "OfertaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Oferta");

            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Status");
        }
    }
}
