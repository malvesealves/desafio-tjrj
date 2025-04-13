using API.Features.Assunto;
using API.Features.Autor;
using API.Features.FormasCompra;
using API.Features.Livros;
using Microsoft.EntityFrameworkCore;

namespace API.DatabaseContext
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<FormaCompra> FormasCompra { get; set; }
    }
}
