using Domain.Models;

namespace Domain.Interfaces
{
    public interface ILivroService
    {
        Task<ResponseModel<IEnumerable<Livro>>> ListarLivrosAsync();
        Task<ResponseModel<Livro>> BuscarLivroPorIdAsync(int id);
        Task<ResponseModel<Livro>> CriarLivroAsync(Livro livro);
        Task<ResponseModel<Livro>> EditarLivroAsync(int id, Livro livro);
        Task<ResponseModel<bool>> ExcluirLivroAsync(int id);
    }
}
