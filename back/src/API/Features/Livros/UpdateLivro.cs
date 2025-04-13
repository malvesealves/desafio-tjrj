using API.DatabaseContext;
using API.Endpoints;

namespace API.Features.Livros
{
    public static class UpdateLivro
    {
        #region Response

        public record Request(string Titulo, string Editora, int Edicao, string AnoPublicacao, short TipoCompra, decimal Preco);
        public record Response(int CodL, string Titulo);

        #endregion

        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapPut("livros/{id}", Handler).WithTags("Livros");
            }

            public static async Task<IResult> Handler(Request request, int id, AppDbContext context)
            {
                Livro? livro = await context.Livros.FindAsync(id);

                if (livro is null)
                {
                    return TypedResults.NotFound();
                }

                livro.Titulo = request.Titulo;
                livro.Editora = request.Editora;
                livro.Edicao = request.Edicao;
                livro.Edicao = request.Edicao;
                livro.AnoPublicacao = request.AnoPublicacao;
                livro.Preco = request.Preco;

                await context.SaveChangesAsync();

                return TypedResults.Ok(new Response(livro.CodL, livro.Titulo));
            }
        }
    }
}
