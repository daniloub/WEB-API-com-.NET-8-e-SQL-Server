using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly AppDbContext _context;
        
        public LivroRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<Livro?> GetByIdAsync(int id)
        {
            return await _context.Livros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(l => l.Id == id);
        }
        
        public async Task<IEnumerable<Livro>> GetAllAsync()
        {
            return await _context.Livros
                .Include(l => l.Autor)
                .ToListAsync();
        }
        
        public async Task<Livro> AddAsync(Livro livro)
        {
            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();
            return livro;
        }
        
        public async Task<Livro> UpdateAsync(Livro livro)
        {
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
            return livro;
        }
        
        public async Task<bool> DeleteAsync(int id)
        {
            var livro = await GetByIdAsync(id);
            if (livro == null) return false;
            
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Livros.AnyAsync(l => l.Id == id);
        }
        
        public async Task<IEnumerable<Livro>> GetLivrosComAutorAsync()
        {
            return await _context.Livros
                .Include(l => l.Autor)
                .ToListAsync();
        }
        
        public async Task<IEnumerable<Livro>> GetLivrosByAutorIdAsync(int autorId)
        {
            return await _context.Livros
                .Include(l => l.Autor)
                .Where(l => l.AutorId == autorId)
                .ToListAsync();
        }
        
        public async Task<Livro?> GetByTituloAsync(string titulo)
        {
            return await _context.Livros
                .Include(l => l.Autor)
                .FirstOrDefaultAsync(l => l.Titulo == titulo);
        }
    }
}
