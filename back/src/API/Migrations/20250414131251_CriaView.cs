using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class CriaView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE VIEW vw_livros_autores AS
                                    SELECT
                                    l.""CodL"" AS livro_id,
                                    l.""Titulo"" AS livro_titulo,
                                    ass.""Descricao"" AS livro_assunto,
                                    l.""Preco"" AS livro_preco,
                                    au.""CodAu"" AS autor_id,
                                    au.""Nome"" AS autor_nome,
                                    ass.""CodAs"" AS assunto_id,
                                    ass.""Descricao"" AS assunto_descricao
                                FROM ""LivrosAutores"" la
                                JOIN ""Livros"" l ON la.""CodL"" = l.""CodL""
                                JOIN ""Autores"" au ON la.""CodAu"" = au.""CodAu""
                                JOIN ""Assuntos"" ass ON l.""AssuntoId"" = ass.""CodAs"";");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS vw_livros_autores;");
        }
    }
}
