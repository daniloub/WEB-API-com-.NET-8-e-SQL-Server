using Domain.Models;

namespace Domain.Interfaces
{
    public interface ILivroRepository : IRepository<Livro>
    {
        Task<IEnumerable<Livro>> GetLivrosComAutorAsync();
        Task<IEnumerable<Livro>> GetLivrosByAutorIdAsync(int autorId);
        Task<Livro?> GetByTituloAsync(string titulo);
    }
}
