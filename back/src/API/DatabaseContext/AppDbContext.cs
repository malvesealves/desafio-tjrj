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
        public DbSet<RelatorioView> ViewLivroAutor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Assunto

            modelBuilder.Entity<Assunto>().ToTable("Assuntos");

            modelBuilder.Entity<Assunto>()
                .HasKey(a => a.CodAs);

            modelBuilder.Entity<Assunto>().Property(a => a.CodAs).HasColumnName("CodAs").ValueGeneratedOnAdd();
            modelBuilder.Entity<Assunto>().Property(a => a.Descricao).HasColumnName("Descricao").IsRequired().HasMaxLength(20);

            #endregion

            #region Autor

            modelBuilder.Entity<Autor>().ToTable("Autores");

            modelBuilder.Entity<Autor>()
                .HasKey(a => a.CodAu);

            modelBuilder.Entity<Autor>().Property(a => a.CodAu).HasColumnName("CodAu").ValueGeneratedOnAdd();
            modelBuilder.Entity<Autor>().Property(a => a.Nome).HasColumnName("Nome").IsRequired().HasMaxLength(40);

            #endregion

            #region Forma Compra

            modelBuilder.Entity<FormaCompra>().ToTable("FormasCompra");

            modelBuilder.Entity<FormaCompra>()
                .HasKey(a => a.CodForComp);

            modelBuilder.Entity<FormaCompra>().Property(a => a.CodForComp).HasColumnName("CodForComp").ValueGeneratedOnAdd();
            modelBuilder.Entity<FormaCompra>().Property(a => a.Descricao).HasColumnName("Descricao").IsRequired().HasMaxLength(20);

            #endregion

            #region Livro

            modelBuilder.Entity<Livro>().ToTable("Livros");

            modelBuilder.Entity<Livro>()
                .HasKey(l => l.CodL);

            modelBuilder.Entity<Livro>().Property(a => a.CodL).HasColumnName("CodL").ValueGeneratedOnAdd();
            modelBuilder.Entity<Livro>().Property(a => a.Titulo).HasColumnName("Titulo");
            modelBuilder.Entity<Livro>().Property(a => a.Editora).HasColumnName("Editora");

            modelBuilder.Entity<Livro>()
                .HasOne(l => l.Assunto)
                .WithMany(a => a.Livros)
                .HasForeignKey(l => l.AssuntoId);

            #endregion

            #region LivroAutor

            modelBuilder.Entity<LivroAutor>().ToTable("LivrosAutores");

            modelBuilder.Entity<LivroAutor>().Property(l => l.CodL).HasColumnName("CodL");
            modelBuilder.Entity<LivroAutor>().Property(l => l.CodAu).HasColumnName("CodAu");

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

            #region View

            modelBuilder.Entity<RelatorioView>()
                .HasNoKey()
                .ToView("vw_livros_autores");

            #endregion
        }
    }
}
