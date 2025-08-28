using WebApi_Video.Dtos;
using WebApi_Video.Models;

namespace WebApi_Video.Services.Livros
{
    public interface ILivroService
    {
        Task<ResponseModel<List<LivroModel>>> ListarLivros();
        Task<ResponseModel<LivroModel>> BuscarLivroPorId(int id);
        Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroDTO livroModel);
        Task<ResponseModel<LivroModel>> EditarLivro(int id, LivroDTO livroModel);
        Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int id);
    }
}
