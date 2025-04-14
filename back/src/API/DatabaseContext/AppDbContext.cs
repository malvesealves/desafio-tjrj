using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.DatabaseContext
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Assunto> Assuntos { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<FormaCompra> FormasCompra { get; set; }
        public DbSet<LivroAutor> LivrosAutores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Assunto

            modelBuilder.Entity<Assunto>()
                .HasKey(a => a.CodAs);

            modelBuilder.Entity<Assunto>().Property(a => a.CodAs).ValueGeneratedOnAdd();

            modelBuilder.Entity<Assunto>().Property(a => a.Descricao).IsRequired().HasMaxLength(40);

            #endregion

            #region Autor

            modelBuilder.Entity<Autor>()
                .HasKey(a => a.CodAu);

            modelBuilder.Entity<Autor>().Property(a => a.CodAu).ValueGeneratedOnAdd();

            #endregion

            #region Forma Compra

            modelBuilder.Entity<FormaCompra>()
                .HasKey(a => a.CodForComp);

            modelBuilder.Entity<FormaCompra>().Property(a => a.CodForComp).ValueGeneratedOnAdd();

            #endregion

            #region Livro

            modelBuilder.Entity<Livro>()
                .HasKey(l => l.CodL);

            modelBuilder.Entity<Livro>().Property(a => a.CodL).ValueGeneratedOnAdd();

            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Assunto)
                .WithMany(a => a.Livros)
                .HasForeignKey(l => l.AssuntoId);

            #endregion

            #region LivroAutor

            modelBuilder.Entity<LivroAutor>()
                .HasKey(la => new { la.CodL, la.CodAu });

            modelBuilder.Entity<LivroAutor>()
                .HasOne(la => la.Livro)
                .WithMany(l => l.LivrosAutores)
                .HasForeignKey(la => la.CodL);

            modelBuilder.Entity<LivroAutor>()
                .HasOne(la => la.Autor)
                .WithMany(a => a.LivrosAutores)
                .HasForeignKey(la => la.CodAu);

            #endregion
        }
    }
}
