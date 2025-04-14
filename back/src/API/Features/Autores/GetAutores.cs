using API.DatabaseContext;
using API.Endpoints;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Autores;

public class GetAutores
{
    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("autores", Handler).WithTags("Autores");
        }

        public static async Task<IResult> Handler(AppDbContext context)
        {
            List<Autor> autores = await context.Autores.ToListAsync();

            return TypedResults.Ok(autores);
        }
    }
}
