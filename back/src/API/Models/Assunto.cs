namespace API.Models;

public class Assunto
{
    public int CodAs { get; set; }
    public string Descricao { get; set; } = string.Empty;

    public ICollection<Livro>? Livros { get; set; } 
}
