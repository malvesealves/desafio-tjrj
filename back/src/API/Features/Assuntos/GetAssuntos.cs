using API.DatabaseContext;
using API.Dto;
using API.Endpoints;
using API.Mapper;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Assuntos;

public class GetAssuntos
{
    #region Response

    public record Response(List<AssuntoDto> Data);

    #endregion

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("assuntos", Handler).WithTags("Assuntos");
        }

        public static async Task<IResult> Handler(AppDbContext context)
        {
            List<AssuntoDto> assuntos = await context.Assuntos
                .OrderBy(a => a.CodAs)
                .Select(a => AssuntoMapper.ToDTO(a))
                .AsNoTracking()
                .ToListAsync();

            if (assuntos.Count > 0)
                return TypedResults.Ok(new Response(assuntos));

            return TypedResults.Ok(new Response([]));
        }
    }
}
