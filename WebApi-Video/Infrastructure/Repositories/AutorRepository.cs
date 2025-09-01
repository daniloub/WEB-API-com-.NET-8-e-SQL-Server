using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly AppDbContext _context;
        
        public AutorRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<Autor?> GetByIdAsync(int id)
        {
            return await _context.Autores
                .Include(a => a.Livros)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        
        public async Task<IEnumerable<Autor>> GetAllAsync()
        {
            return await _context.Autores
                .Include(a => a.Livros)
                .ToListAsync();
        }
        
        public async Task<Autor> AddAsync(Autor autor)
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
            return autor;
        }
        
        public async Task<Autor> UpdateAsync(Autor autor)
        {
            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();
            return autor;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var autor = await GetByIdAsync(id);
            if (autor == null) return false;
            
            if (autor.PossuiLivros)
                throw new InvalidOperationException("Não é possível excluir um autor que possui livros.");
            
            _context.Autores.Remove(autor);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Autores.AnyAsync(a => a.Id == id);
        }
        
        public async Task<Autor?> GetByNomeCompletoAsync(string nome, string sobrenome)
        {
            return await _context.Autores
                .FirstOrDefaultAsync(a => a.Nome == nome && a.Sobrenome == sobrenome);
        }
        
        public async Task<IEnumerable<Autor>> GetAutoresComLivrosAsync()
        {
            return await _context.Autores
                .Include(a => a.Livros)
                .Where(a => a.Livros.Any())
                .ToListAsync();
        }
        
        public async Task<Autor?> GetAutorByLivroIdAsync(int livroId)
        {
            var livro = await _context.Livros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(l => l.Id == livroId);
                
            return livro?.Autor;
        }
    }
}
