using Domain.Models;

namespace Domain.Interfaces
{
    public interface IAutorService
    {
        Task<ResponseModel<IEnumerable<Autor>>> ListarAutoresAsync();
        Task<ResponseModel<Autor>> BuscarAutorPorIdAsync(int id);
        Task<ResponseModel<Autor>> BuscarAutorPorIdLivroAsync(int idLivro);
        Task<ResponseModel<Autor>> CriarAutorAsync(Autor autor);
        Task<ResponseModel<Autor>> EditarAutorAsync(int id, Autor autor);
        Task<ResponseModel<bool>> ExcluirAutorAsync(int id);
    }
}
