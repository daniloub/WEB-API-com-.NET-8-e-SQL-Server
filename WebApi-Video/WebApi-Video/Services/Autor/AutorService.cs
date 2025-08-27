using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApi_Video.Data;
using WebApi_Video.Models;

namespace WebApi_Video.Services.Autor
{
    public class AutorService : IAutorService
    {
        private readonly AppDbContext _dbContext;
        public AutorService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<ResponseModel<AutorModel>> BuscarAutorPorId(int id)
        {
            ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();
            var autor = await _dbContext.Autores.FirstOrDefaultAsync(x => x.Id == id);
            if(autor == null)
            {
                response.Mensagem = "Nenhum registro localizado";
                response.Status = false;
                return response;
            }

            response.Dados = autor;
            response.Mensagem = "Autor localizado!";

            return response;
        }

        public Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<AutorModel>>> ListarAutores()
        {
           ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();
            try
            {
                var autores = await _dbContext.Autores.ToListAsync();
                if (autores.Count == 0)
                {
                    response.Mensagem = "Nenhum autor encontrado.";
                    response.Status = false;
                    return response;
                }
                response.Dados = autores;
                response.Mensagem = "Autores encontrados com sucesso.";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao listar autores: {ex.Message}";
                response.Status = false;
                return response;
            }
        }
    }
}
