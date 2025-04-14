using API.DatabaseContext;
using API.Endpoints;
using API.Models;
using FluentValidation;
using FluentValidation.Results;

namespace API.Features.Livros;

public static class CreateLivro
{
    #region Request e Response

    public record Request(string Titulo, int AssuntoId, int[] AutoresIds, string Editora, int Edicao, string AnoPublicacao, int FormaCompra, decimal Preco);
    public record Response(int CodL, string Titulo);

    #endregion

    #region Validator    

    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(r => r.Titulo).NotEmpty().MaximumLength(40);
            RuleFor(r => r.AssuntoId).GreaterThan(0);
            RuleFor(r => r.AutoresIds).NotNull();
            RuleFor(r => r.Editora).NotEmpty().MaximumLength(40);
            RuleFor(r => r.Edicao).GreaterThan(0);
            RuleFor(r => r.AnoPublicacao).NotEmpty().MaximumLength(4);
            RuleFor(r => r.FormaCompra).NotEmpty();
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

            Assunto? assunto = await context.Assuntos.FindAsync(request.AssuntoId);

            if (assunto is null)
                return TypedResults.BadRequest(Messages.Messages.Assunto_NaoInformadoOuInvalido);

            IQueryable<Autor> autores = context.Autores.Where(a => request.AutoresIds.Contains(a.CodAu));

            if (!autores.Any())
                return TypedResults.BadRequest(Messages.Messages.Autor_NaoInformadoOuInvalido);

            FormaCompra? formaCompra = await context.FormasCompra.FindAsync(request.FormaCompra);

            if (formaCompra is null)
                return TypedResults.BadRequest(Messages.Messages.FormaCompra_NaoInformadoOuInvalido);

            Livro livro = new()
            {
                Titulo = request.Titulo,
                Assunto = assunto,
                Editora = request.Editora,
                AnoPublicacao = request.AnoPublicacao,
                FormaCompra = formaCompra,
                Preco = request.Preco
            };

            context.LivrosAutores.AddRange([.. CriarLivroAutor(livro, autores)]);

            context.Livros.Add(livro);

            await context.SaveChangesAsync();

            return TypedResults.Ok(new Response(livro.CodL, livro.Titulo));
        }

        private static IEnumerable<LivroAutor> CriarLivroAutor(Livro livro, IQueryable<Autor> autores)
        {
            foreach (Autor autor in autores)
            {
                yield return new LivroAutor()
                {
                    Livro = livro,
                    Autor = autor
                };
            }
        }
    }
}
