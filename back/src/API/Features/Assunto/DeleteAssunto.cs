using API.DatabaseContext;
using API.Endpoints;

namespace API.Features.Assunto
{
    public class DeleteAssunto
    {
        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapDelete("assuntos/{id}", Handler).WithTags("Assuntos");
            }

            public static async Task<IResult> Handler(int id, AppDbContext context)
            {
                Assunto? assunto = await context.Assuntos.FindAsync(id);

                if (assunto is null)
                {
                    return TypedResults.NotFound();
                }

                context.Remove(assunto);

                await context.SaveChangesAsync();

                return TypedResults.NoContent();
            }
        }
    }
}
