using Domain.Models;

namespace Domain.Interfaces
{
    public interface IAutorRepository : IRepository<Autor>
    {
        Task<Autor?> GetByNomeCompletoAsync(string nome, string sobrenome);
        Task<IEnumerable<Autor>> GetAutoresComLivrosAsync();
        Task<Autor?> GetAutorByLivroIdAsync(int livroId);
    }
}
