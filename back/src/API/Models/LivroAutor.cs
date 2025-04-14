namespace API.Models
{
    public class LivroAutor
    {
        public int CodL { get; set; }
        public required Livro Livro { get; set; }

        public int CodAu { get; set; }
        public required Autor Autor { get; set; }
    }
}
