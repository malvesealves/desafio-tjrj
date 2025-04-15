using API.DatabaseContext;
using API.Dto;
using API.Endpoints;
using API.Mapper;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Features.Livros;

public static class GetLivros
{
    #region Response

    public record Response(List<LivroDto> Data);

    #endregion

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("livros", Handler).WithTags("Livros");
        }

        public static async Task<IResult> Handler(AppDbContext context)
        {
            List<LivroDto> livros = await context.Livros
                .Include(l => l.FormaCompra)
                .Include(l => l.Assunto)
                .OrderBy(l => l.CodL)
                .Select(l => LivroMapper.ToDTO(l))                
                .AsNoTracking()
                .ToListAsync();

            if (livros.Count > 0)
                return TypedResults.Ok(new Response(livros));

            return TypedResults.Ok(new Response([]));
        }
    }
}
