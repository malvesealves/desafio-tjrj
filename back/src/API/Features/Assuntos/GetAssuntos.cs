using API.DatabaseContext;
using API.Endpoints;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Assuntos;

public class GetAssuntos
{
    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("assuntos", Handler).WithTags("Assuntos");
        }

        public static async Task<IResult> Handler(AppDbContext context)
        {
            List<Assunto> assuntos = await context.Assuntos.ToListAsync();

            return TypedResults.Ok(assuntos.Select(a => { a.CodAs, a.Descricao}));
        }
    }
}
