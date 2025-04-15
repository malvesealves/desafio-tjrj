namespace API.Dto
{
    public class LivroDto
    {
        public int CodL { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int Edicao { get; set; }
        public string AnoPublicacao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public string Assunto { get; set; } = string.Empty;
        public string FormaCompra { get; set; } = string.Empty;
    }
}
