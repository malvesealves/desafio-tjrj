using API.DatabaseContext;
using API.Dto;
using API.Endpoints;
using API.Mapper;
using Microsoft.EntityFrameworkCore;

namespace API.Features.FormasCompra
{
    #region Response

    public record Response(List<FormaCompraDto> Data);

    #endregion

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
                List<FormaCompraDto> formasCompra = await context.FormasCompra
                    .OrderBy(f => f.CodForComp)
                    .Select(f => FormaCompraMapper.ToDTO(f))                    
                    .AsNoTracking()
                    .ToListAsync();

                if (formasCompra.Count > 0)
                    return TypedResults.Ok(new Response(formasCompra));

                return TypedResults.Ok(new Response([]));
            }
        }
    }
}
