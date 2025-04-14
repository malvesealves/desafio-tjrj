using API.DatabaseContext;
using API.Endpoints;
using API.Models;
using FluentValidation;
using FluentValidation.Results;

namespace API.Features.Livros
{
    public static class UpdateLivro
    {
        #region Response

        public record Request(string Titulo, int AssuntoId, int[] AutoresIds, string Editora, int Edicao, string AnoPublicacao, short TipoCompra, decimal Preco);
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
                RuleFor(r => r.TipoCompra).NotEmpty();
                RuleFor(r => r.Preco).GreaterThan(0);
            }
        }

        #endregion

        public class Endpoint : IEndpoint
        {
            public void MapEndpoint(IEndpointRouteBuilder app)
            {
                app.MapPut("livros/{id}", Handler).WithTags("Livros");
            }

            public static async Task<IResult> Handler(Request request, int id, AppDbContext context, IValidator<Request> validator)
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

                Livro? livro = await context.Livros.FindAsync(id);

                if (livro is null)
                    return TypedResults.NotFound();

                livro.Titulo = request.Titulo;
                livro.Editora = request.Editora;
                livro.Edicao = request.Edicao;
                livro.Edicao = request.Edicao;
                livro.AnoPublicacao = request.AnoPublicacao;
                livro.Preco = request.Preco;

                IQueryable<LivroAutor> livroAutores = context.LivrosAutores.Where(l => l.CodL == livro.CodL);

                livro.LivrosAutores = [.. AtualizarLivroAutor(livro, autores)];

                await context.SaveChangesAsync();

                return TypedResults.Ok(new Response(livro.CodL, livro.Titulo));
            }

            private static IEnumerable<LivroAutor> AtualizarLivroAutor(Livro livro, IQueryable<Autor> autores)
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
}
