using API.DatabaseContext;
using API.Endpoints;

namespace API.Features.Livros
{
    public class DeleteLivro
    {
        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapDelete("livros/{id}", Handler).WithTags("Livros");
            }

            public static async Task<IResult> Handler(int id, AppDbContext context)
            {
                Livro? livro = await context.Livros.FindAsync(id);

                if (livro is null)
                {
                    return TypedResults.NotFound();
                }

                context.Remove(livro);

                await context.SaveChangesAsync();

                return TypedResults.NoContent();

            }
        }
    }
}
