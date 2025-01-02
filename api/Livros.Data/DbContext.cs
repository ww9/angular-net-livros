using Microsoft.EntityFrameworkCore;
using Livros.Data.Entities;

namespace Livros.Data;

public class LivrosContext : DbContext
{
    public DbSet<Livro> Livros { get; set; }
    public DbSet<Autor> Autores { get; set; }
    public DbSet<Assunto> Assuntos { get; set; }
    public DbSet<LivroAutor> LivroAutores { get; set; }
    public DbSet<LivroAssunto> LivroAssuntos { get; set; }

    public LivrosContext(DbContextOptions<LivrosContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
