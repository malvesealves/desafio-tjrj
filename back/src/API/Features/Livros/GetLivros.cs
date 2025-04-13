using API.DatabaseContext;
using API.Endpoints;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Livros;

public static class GetLivros
{
    #region Response

    public record Response(Livro Livro);

    #endregion

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("livros", Handler).WithTags("Livros");
        }

        public static async Task<IResult> Handler(AppDbContext context)
        {
            List<Livro> livros = await context.Livros.ToListAsync();

            List<Response> response = [.. livros.Select(l => new Response(l))];

            return TypedResults.Ok(response);
        }
    }
}
