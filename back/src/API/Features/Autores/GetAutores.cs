using API.DatabaseContext;
using API.Dto;
using API.Endpoints;
using API.Mapper;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Autores;

public class GetAutores
{
    #region Response

    public record Response(List<AutorDto> Data);

    #endregion

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("autores", Handler).WithTags("Autores");
        }

        public static async Task<IResult> Handler(AppDbContext context)
        {
            List<AutorDto> autores = await context.Autores
                .OrderBy(a => a.CodAu)
                .Select(a => AutorMapper.ToDTO(a))                
                .AsNoTracking()
                .ToListAsync();

            if (autores.Count > 0)
                return TypedResults.Ok(new Response(autores));

            return TypedResults.Ok(new Response([]));
        }
    }
}
