using API.DatabaseContext;
using API.Endpoints;
using Microsoft.EntityFrameworkCore;

namespace API.Features.FormasCompra
{
    public class GetFormasCompra
    {
        #region Response

        public record Response(FormaCompra FormaCompra);

        #endregion

        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapGet("formascompra", Handler).WithTags("FormasCompra");
            }

            public static async Task<IResult> Handler(AppDbContext context)
            {
                List<FormaCompra> livros = await context.FormasCompra.ToListAsync();

                List<Response> response = [.. livros.Select(l => new Response(l))];

                return TypedResults.Ok(response);
            }
        }
    }
}
