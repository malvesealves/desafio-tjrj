namespace API.Models;

public class Livro
{
    public int CodL { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Editora { get; set; } = string.Empty;
    public int Edicao { get; set; }
    public string AnoPublicacao { get; set; } = string.Empty;
    public short TipoCompra { get; set; }
    public decimal Preco { get; set; }

    public int AssuntoId { get; set; }
    public required Assunto Assunto { get; set; }
    public ICollection<LivroAutor>? LivrosAutores { get; set; }
}