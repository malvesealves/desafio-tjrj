using API.DatabaseContext;
using API.Endpoints;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Features.FormasCompra
{
    public class GetFormasCompra
    {
        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapGet("formas-compra", Handler).WithTags("FormasCompra");
            }

            public static async Task<IResult> Handler(AppDbContext context)
            {
                List<FormaCompra> livros = await context.FormasCompra.ToListAsync();

                return TypedResults.Ok(livros);
            }
        }
    }
}
