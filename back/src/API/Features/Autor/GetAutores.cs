using API.DatabaseContext;
using API.Endpoints;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Autor
{
    public class GetAutores
    {
        #region Response

        public record Response(Autor Autor);

        #endregion

        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapGet("autores", Handler).WithTags("Autores");
            }

            public static async Task<IResult> Handler(AppDbContext context)
            {
                List<Autor> autores = await context.Autores.ToListAsync();

                List<Response> response = [.. autores.Select(l => new Response(l))];

                return TypedResults.Ok(response);
            }
        }
    }
}
