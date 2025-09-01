using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IAutorRepository _autorRepository;
        
        public LivroService(ILivroRepository livroRepository, IAutorRepository autorRepository)
        {
            _livroRepository = livroRepository;
            _autorRepository = autorRepository;
        }
        
        public async Task<ResponseModel<IEnumerable<Livro>>> ListarLivrosAsync()
        {
            try
            {
                var livros = await _livroRepository.GetLivrosComAutorAsync();
                
                if (!livros.Any())
                    return ResponseModel<IEnumerable<Livro>>.NotFound("Nenhum livro encontrado.");
                
                return ResponseModel<IEnumerable<Livro>>.Success(livros, "Livros encontrados com sucesso.");
            }
            catch (Exception ex)
            {
                return ResponseModel<IEnumerable<Livro>>.Error($"Erro ao listar livros: {ex.Message}");
            }
        }
        
        public async Task<ResponseModel<Livro>> BuscarLivroPorIdAsync(int id)
        {
            try
            {
                var livro = await _livroRepository.GetByIdAsync(id);
                
                if (livro == null)
                    return ResponseModel<Livro>.NotFound($"Livro com ID {id} não encontrado.");
                
                return ResponseModel<Livro>.Success(livro, "Livro encontrado com sucesso.");
            }
            catch (Exception ex)
            {
                return ResponseModel<Livro>.Error($"Erro ao buscar livro: {ex.Message}");
            }
        }
        
        public async Task<ResponseModel<Livro>> CriarLivroAsync(Livro livro)
        {
            try
            {
                // Verificar se o autor existe
                var autor = await _autorRepository.GetByIdAsync(livro.AutorId);
                if (autor == null)
                    return ResponseModel<Livro>.Error($"Autor com ID {livro.AutorId} não encontrado.");
                
                // Verificar se já existe livro com mesmo título
                var livroExistente = await _livroRepository.GetByTituloAsync(livro.Titulo);
                if (livroExistente != null)
                    return ResponseModel<Livro>.Error("Já existe um livro com este título.");
                
                livro.AtribuirAutor(autor);
                var livroCriado = await _livroRepository.AddAsync(livro);
                
                return ResponseModel<Livro>.Success(livroCriado, "Livro criado com sucesso.");
            }
            catch (Exception ex)
            {
                return ResponseModel<Livro>.Error($"Erro ao criar livro: {ex.Message}");
            }
        }
        
        public async Task<ResponseModel<Livro>> EditarLivroAsync(int id, Livro livro)
        {
            try
            {
                var livroExistente = await _livroRepository.GetByIdAsync(id);
                if (livroExistente == null)
                    return ResponseModel<Livro>.NotFound($"Livro com ID {id} não encontrado.");
                
                // Verificar se o novo autor existe
                var autor = await _autorRepository.GetByIdAsync(livro.AutorId);
                if (autor == null)
                    return ResponseModel<Livro>.Error($"Autor com ID {livro.AutorId} não encontrado.");
                
                // Verificar se já existe outro livro com mesmo título
                var livroComMesmoTitulo = await _livroRepository.GetByTituloAsync(livro.Titulo);
                if (livroComMesmoTitulo != null && livroComMesmoTitulo.Id != id)
                    return ResponseModel<Livro>.Error("Já existe outro livro com este título.");
                
                livroExistente.AtualizarTitulo(livro.Titulo);
                livroExistente.AtribuirAutor(autor);
                
                var livroAtualizado = await _livroRepository.UpdateAsync(livroExistente);
                return ResponseModel<Livro>.Success(livroAtualizado, "Livro atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return ResponseModel<Livro>.Error($"Erro ao editar livro: {ex.Message}");
            }
        }
        
        public async Task<ResponseModel<bool>> ExcluirLivroAsync(int id)
        {
            try
            {
                var livro = await _livroRepository.GetByIdAsync(id);
                if (livro == null)
                    return ResponseModel<bool>.NotFound($"Livro com ID {id} não encontrado.");
                
                var resultado = await _livroRepository.DeleteAsync(id);
                return ResponseModel<bool>.Success(resultado, "Livro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return ResponseModel<bool>.Error($"Erro ao excluir livro: {ex.Message}");
            }
        }
    }
}
