using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Models;
using WebApi.Services.Autor;

namespace WebApi.Services.Autor
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
            if (autor == null)
            {
                response.Mensagem = "Nenhum registro localizado";
                response.Status = false;
                return response;
            }

            response.Dados = autor;
            response.Mensagem = "Autor localizado!";

            return response;
        }

        public async Task<ResponseModel<AutorModel>> BuscarAutorPorIdLivro(int idLivro)
        {
            ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();
            try
            {
                var livro = await _dbContext.Livros
                    .Include(l => l.Autor)
                    .Where(l => l.Id == idLivro)
                    .Select(l => l.Autor)
                    .FirstOrDefaultAsync();
                if (livro == null)
                {
                    response.Mensagem = "Livro não encontrado";
                    response.Status = false;
                    return response;
                }
                response.Dados = livro;
                response.Mensagem = "Autor encontrado com sucesso.";
                return response;

            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao buscar autor por Id: {ex.Message}";
                response.Status = false;
                return response;


            }
        }

        public async Task<ResponseModel<List<AutorModel>>> CriarAutor(AutorDTO autorDTO)
        {
            ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();

            try
            {
                var autor = new AutorModel()
                {
                    Nome = autorDTO.Nome,
                    Sobrenome = autorDTO.Sobrenome
                };
                
                _dbContext.Autores.Add(autor);
                await _dbContext.SaveChangesAsync(); 

                var autores = await ListarAutores();

                response.Dados = autores.Dados;
                response.Mensagem = "Autor criado com sucesso.";
                response.Status = true;

                return response;  
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao criar autor: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<AutorModel>> EditarAutor(int id, AutorDTO autorDTO)
        {
            ResponseModel<AutorModel> response = new ResponseModel<AutorModel>();
            try
            {
                var autor = _dbContext.Autores.FirstOrDefault(x => x.Id == id);
                if (autor == null)
                {
                    response.Mensagem = "Autor não encontrado.";
                    response.Status = false;
                    return response;
                }
                autor.Nome = autorDTO.Nome;
                autor.Sobrenome = autorDTO.Sobrenome;
                _dbContext.Autores.Update(autor);
                await _dbContext.SaveChangesAsync();

                response.Dados = autor;
                response.Mensagem = "Autor editado com sucesso.";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = $"Erro ao editar autor: {ex.Message}";
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<AutorModel>>> ExcluirAutor(int id)
        {
            ResponseModel<List<AutorModel>> response = new ResponseModel<List<AutorModel>>();

            try
            {
                var autor = _dbContext.Autores.FirstOrDefault(x => x.Id == id);
                if (autor == null)
                {
                    response.Mensagem = "Autor não encontrado.";
                    response.Status = false;
                    return response;
                }

                _dbContext.Autores.Remove(autor);
                await _dbContext.SaveChangesAsync();

                var autores = await ListarAutores();

                response.Dados = autores.Dados;
                response.Mensagem = "Autor removido com sucesso.";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
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
