using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class CriaTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assuntos",
                columns: table => new
                {
                    CodAs = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assuntos", x => x.CodAs);
                });

            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    CodAu = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.CodAu);
                });

            migrationBuilder.CreateTable(
                name: "FormasCompra",
                columns: table => new
                {
                    CodForComp = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Descricao = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormasCompra", x => x.CodForComp);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    CodL = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Editora = table.Column<string>(type: "text", nullable: false),
                    Edicao = table.Column<int>(type: "integer", nullable: false),
                    AnoPublicacao = table.Column<string>(type: "text", nullable: false),
                    Preco = table.Column<decimal>(type: "numeric", nullable: false),
                    AssuntoId = table.Column<int>(type: "integer", nullable: false),
                    FormaCompraId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.CodL);
                    table.ForeignKey(
                        name: "FK_Livros_Assuntos_AssuntoId",
                        column: x => x.AssuntoId,
                        principalTable: "Assuntos",
                        principalColumn: "CodAs",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livros_FormasCompra_FormaCompraId",
                        column: x => x.FormaCompraId,
                        principalTable: "FormasCompra",
                        principalColumn: "CodForComp",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LivrosAutores",
                columns: table => new
                {
                    CodL = table.Column<int>(type: "integer", nullable: false),
                    CodAu = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivrosAutores", x => new { x.CodL, x.CodAu });
                    table.ForeignKey(
                        name: "FK_LivrosAutores_Autores_CodAu",
                        column: x => x.CodAu,
                        principalTable: "Autores",
                        principalColumn: "CodAu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LivrosAutores_Livros_CodL",
                        column: x => x.CodL,
                        principalTable: "Livros",
                        principalColumn: "CodL",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livros_AssuntoId",
                table: "Livros",
                column: "AssuntoId");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_FormaCompraId",
                table: "Livros",
                column: "FormaCompraId");

            migrationBuilder.CreateIndex(
                name: "IX_LivrosAutores_CodAu",
                table: "LivrosAutores",
                column: "CodAu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivrosAutores");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "Assuntos");

            migrationBuilder.DropTable(
                name: "FormasCompra");
        }
    }
}
