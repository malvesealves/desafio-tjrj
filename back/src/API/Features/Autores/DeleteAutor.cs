using API.DatabaseContext;
using API.Endpoints;
using API.Models;

namespace API.Features.Autores;

public class DeleteAutor
{
    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapDelete("autores/{id}", Handler).WithTags("Autores");
        }

        public static async Task<IResult> Handler(int id, AppDbContext context)
        {
            Autor? autor = await context.Autores.FindAsync(id);

            if (autor is null)
            {
                return TypedResults.NotFound();
            }

            context.Remove(autor);

            await context.SaveChangesAsync();

            return TypedResults.NoContent();
        }
    }
}
