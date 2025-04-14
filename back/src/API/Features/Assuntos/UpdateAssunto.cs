using API.DatabaseContext;
using API.Endpoints;
using API.Models;
using FluentValidation;
using FluentValidation.Results;

namespace API.Features.Assuntos;

public class UpdateAssunto
{
    #region Response

    public record Request(string Descricao);
    public record Response(int CodAs, string Descricao);

    #endregion

    #region Validator    

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Descricao).NotEmpty().MaximumLength(40);
        }
    }

    #endregion

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPut("assuntos/{id}", Handler).WithTags("Assuntos");
        }

        public static async Task<IResult> Handler(Request request, int id, AppDbContext context, IValidator<Request> validator)
        {
            ValidationResult validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return TypedResults.BadRequest(validationResult.Errors);

            Assunto? assunto = await context.Assuntos.FindAsync(id);

            if (assunto is null)
                return TypedResults.NotFound();

            assunto.Descricao = request.Descricao;

            await context.SaveChangesAsync();

            return TypedResults.Ok(new Response(assunto.CodAs, assunto.Descricao));
        }
    }
}
