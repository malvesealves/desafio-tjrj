namespace API.Models;

public class Autor
{
    public int CodAu { get; set; }
    public string Nome { get; set; } = string.Empty;

    public ICollection<LivroAutor>? LivrosAutores { get; set; }
}
