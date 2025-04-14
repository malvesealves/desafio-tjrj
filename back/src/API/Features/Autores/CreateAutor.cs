using API.DatabaseContext;
using API.Endpoints;
using API.Models;
using FluentValidation;
using FluentValidation.Results;

namespace API.Features.Autores;

public class CreateAutor
{
    #region Request e Response

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
            app.MapPost("autores", Handler).WithTags("Autores");
        }

        public static async Task<IResult> Handler(Request request, AppDbContext context, IValidator<Request> validator)
        {
            ValidationResult validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return TypedResults.BadRequest(validationResult.Errors);

            Autor autor = new()
            {
                Nome = request.Nome
            };

            await context.Autores.AddAsync(autor);

            return TypedResults.Ok(new Response(autor.CodAu, autor.Nome));
        }
    }
}
