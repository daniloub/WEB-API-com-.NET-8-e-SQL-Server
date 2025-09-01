using WebApi.Dtos;
using WebApi.Models;

namespace WebApi.Services.Autor
{
    public interface IAutorService
    {
        //Assincrono<Tipo de retorno<List<Modelo>>>
        Task<ResponseModel<List<AutorModel>>> ListarAutores();
        Task<ResponseModel<AutorModel>> BuscarAutorPorId(int id);
        Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);
        Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorDTO autorDTO);
        Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int id);
        Task<ResponseModel<AutorModel>> EditarAutor(int id, AutorDTO autorDTO);
    }
}
