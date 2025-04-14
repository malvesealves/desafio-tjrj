using API.DatabaseContext;
using API.Endpoints;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Livros;

public static class GetLivros
{
    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("livros", Handler).WithTags("Livros");
        }

        public static async Task<IResult> Handler(AppDbContext context)
        {
            List<Livro> livros = await context.Livros.ToListAsync();

            return TypedResults.Ok(livros);
        }
    }
}
