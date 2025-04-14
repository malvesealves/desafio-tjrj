namespace API.Models
{
    public class RelatorioView
    {
        public int Livro_Id { get; set; }
        public string Livro_Titulo { get; set; } = "";
        public int Autor_Id { get; set; }
        public string Autor_Nome { get; set; } = "";
        public int Assunto_Id { get; set; }
        public string Assunto_Nome { get; set; } = "";
    }
}
