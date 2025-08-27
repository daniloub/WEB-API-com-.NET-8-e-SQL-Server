using WebApi_Video.Models;

namespace WebApi_Video.Services.Autor
{
    public class AutorService : IAutorService
    {
        public Task<ResponseModel<AutorModel>> BuscarAutorPorId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
            throw new NotImplementedException();
        }
    }
}
