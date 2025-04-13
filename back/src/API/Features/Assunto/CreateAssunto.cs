using API.DatabaseContext;
using API.Endpoints;
using FluentValidation;
using FluentValidation.Results;

namespace API.Features.Assunto
{
    public class CreateAssunto
    {
        #region Request e Response

        public record Request(string Descricao);
        public record Response(int CodAs, string Descricao);

        #endregion

        #region Validator    

        public sealed class Validator : AbstractValidator<Request>
        {
            public Validator()
            {
                RuleFor(r => r.Descricao).NotEmpty().MaximumLength(20);
            }
        }

        #endregion

        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapPost("assuntos", Handler).WithTags("Assuntos");
            }

            public static async Task<IResult> Handler(Request request, AppDbContext context, IValidator<Request> validator)
            {
                ValidationResult validationResult = await validator.ValidateAsync(request);

                if (!validationResult.IsValid)
                    return TypedResults.BadRequest(validationResult.Errors);

                Assunto assunto = new()
                {
                    Descricao = request.Descricao
                };

                await context.Assuntos.AddAsync(assunto);

                return TypedResults.Ok(new Response(assunto.CodAs, assunto.Descricao));
            }
        }
    }
}
