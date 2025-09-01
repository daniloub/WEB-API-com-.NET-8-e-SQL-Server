using Microsoft.EntityFrameworkCore;
using Domain.Models;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configuração do Autor
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Sobrenome).IsRequired().HasMaxLength(100);
                
                // Índice para busca por nome
                entity.HasIndex(e => new { e.Nome, e.Sobrenome }).IsUnique();
            });
            
            // Configuração do Livro
            modelBuilder.Entity<Livro>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
                
                // Relacionamento com Autor
                entity.HasOne(e => e.Autor)
                      .WithMany(e => e.Livros)
                      .HasForeignKey(e => e.AutorId)
                      .OnDelete(DeleteBehavior.Restrict);
                
                // Índice para busca por título
                entity.HasIndex(e => e.Titulo);
            });
        }
    }
}
