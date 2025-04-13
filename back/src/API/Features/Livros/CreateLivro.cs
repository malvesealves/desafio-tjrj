using API.DatabaseContext;
using API.Endpoints;
using FluentValidation;
using FluentValidation.Results;

namespace API.Features.Livros;

public static class CreateLivro
{
    #region Request e Response

    public record Request(string Titulo, string Editora, int Edicao, string AnoPublicacao, short TipoCompra, decimal Preco);
    public record Response(int CodL, string Titulo);

    #endregion

    #region Validator    

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Titulo).NotEmpty().MaximumLength(40);
            RuleFor(r => r.Editora).NotEmpty().MaximumLength(40);
            RuleFor(r => r.Edicao).GreaterThan(0);
            RuleFor(r => r.AnoPublicacao).NotEmpty().MaximumLength(4);
            RuleFor(r => r.TipoCompra).NotEmpty();
            RuleFor(r => r.Preco).GreaterThan(0);
        }
    }

    #endregion

    public class Endpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            app.MapPost("livros", Handler).WithTags("Livros");
        }

        public static async Task<IResult> Handler(Request request, AppDbContext context, IValidator<Request> validator)
        {
            ValidationResult validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                return TypedResults.BadRequest(validationResult.Errors);

            Livro livro = new()
            {
                Titulo = request.Titulo,
                Editora = request.Editora,
                AnoPublicacao = request.AnoPublicacao,
                TipoCompra = request.TipoCompra,
                Preco = request.Preco
            };

            await context.Livros.AddAsync(livro);

            return TypedResults.Ok(new Response(livro.CodL, livro.Titulo));
        }
    }
}
