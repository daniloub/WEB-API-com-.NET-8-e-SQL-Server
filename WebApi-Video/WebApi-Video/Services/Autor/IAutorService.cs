using WebApi_Video.Dtos;
using WebApi_Video.Models;

namespace WebApi_Video.Services.Autor
{
    public interface IAutorService
    {
        //Assincrono<Tipo de retorno<List<Modelo>>>
        Task<ResponseModel<List<AutorModel>>> ListarAutores();
        Task<ResponseModel<AutorModel>> BuscarAutorPorId(int id);
        Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro);
        Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorDTO autorDTO);
    }
}
