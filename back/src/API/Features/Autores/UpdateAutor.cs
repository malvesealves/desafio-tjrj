using API.DatabaseContext;
using API.Endpoints;
using API.Models;
using FluentValidation;
using FluentValidation.Results;

namespace API.Features.Autores;

public class UpdateAutor
{
    #region Response

    public record Request(string Nome);
    public record Response(int CodAu, string Nome);

    #endregion

    #region Validator    

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Nome).NotEmpty().MaximumLength(40);
        }
    }

    #endregion

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("autores/{id}", Handler).WithTags("Autores");
        }

        public static async Task<IResult> Handler(Request request, int id, AppDbContext context, IValidator<Request> validator)
        {
            ValidationResult validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return TypedResults.BadRequest(validationResult.Errors);

            Autor? autor = await context.Autores.FindAsync(id);

            if (autor is null)
                return TypedResults.NotFound();

            autor.Nome = request.Nome;

            await context.SaveChangesAsync();

            return TypedResults.Ok(new Response(autor.CodAu, autor.Nome));
        }
    }
}
