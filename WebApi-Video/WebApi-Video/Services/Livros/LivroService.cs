using Microsoft.EntityFrameworkCore;
using WebApi_Video.Data;
using WebApi_Video.Dtos;
using WebApi_Video.Models;

namespace WebApi_Video.Services.Livros
{
    public class LivroService : ILivroService
    {
        private readonly AppDbContext _dbContext;
        public LivroService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<ResponseModel<LivroModel>> BuscarLivroPorId(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<LivroModel>>> CriarLivro(LivroDTO livroDTO)
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();
            var autor = _dbContext.Autores.FirstOrDefault(x => x.Id == livroDTO.AutorId);

            try
            {
                if (autor == null)
                {
                    response.Mensagem = "Autor não encontrado";
                    response.Status = false;
                    return response;
                }
                LivroModel livro = new LivroModel()
                {
                    Titulo = livroDTO.Titulo,
                    Autor = autor,
                };

                _dbContext.Livros.Add(livro);
                await _dbContext.SaveChangesAsync();

                var livros = await _dbContext.Livros.ToListAsync();

                response.Dados = livros;
                response.Mensagem = "Livro criado com sucesso";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao criar livro: {ex.Message}";
                response.Status = false;
                return response;

            }
        }

        public Task<ResponseModel<LivroModel>> EditarLivro(int id, LivroDTO livroModel)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<LivroModel>>> ExcluirLivro(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<List<LivroModel>>> ListarLivros()
        {
            ResponseModel<List<LivroModel>> response = new ResponseModel<List<LivroModel>>();

            var livros = await _dbContext.Livros.Include(l => l.Autor).ToListAsync();
            response.Dados = livros;
            response.Mensagem = "Lista de livros carregada com sucesso";

            return response;
        }
    }
}
