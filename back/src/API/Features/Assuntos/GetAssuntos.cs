using API.DatabaseContext;
using API.Endpoints;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Assuntos;

public class GetAssuntos
{
    #region Response

    public record Response(Assunto Assunto);

    #endregion

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("assuntos", Handler).WithTags("Assuntos");
        }

        public static async Task<IResult> Handler(AppDbContext context)
        {
            List<Assunto> assuntos = await context.Assuntos.ToListAsync();

            List<Response> response = [.. assuntos.Select(l => new Response(l))];

            return TypedResults.Ok(response);
        }
    }
}
