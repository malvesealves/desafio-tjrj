using API.Dto;
using API.Models;

namespace API.Mapper
{
    public static class LivroMapper
    {
        public static LivroDto ToDTO(Livro livro)
        {
            return new LivroDto
            {
                CodL = livro.CodL,
                Titulo = livro.Titulo,
                Editora = livro.Editora,
                Edicao = livro.Edicao,
                AnoPublicacao = livro.AnoPublicacao,
                Preco = livro.Preco,
                Assunto = livro.Assunto.Descricao,
                FormaCompra = livro.FormaCompra.Descricao
            };
        }
    }
}
