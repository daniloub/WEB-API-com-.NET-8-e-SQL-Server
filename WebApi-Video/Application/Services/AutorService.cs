using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;
        private readonly ILivroRepository _livroRepository;
        
        public AutorService(IAutorRepository autorRepository, ILivroRepository livroRepository)
        {
            _autorRepository = autorRepository;
            _livroRepository = livroRepository;
        }
        
        public async Task<ResponseModel<IEnumerable<Autor>>> ListarAutoresAsync()
        {
            try
            {
                var autores = await _autorRepository.GetAllAsync();
                
                if (!autores.Any())
                    return ResponseModel<IEnumerable<Autor>>.NotFound("Nenhum autor encontrado.");
                
                return ResponseModel<IEnumerable<Autor>>.Success(autores, "Autores encontrados com sucesso.");
            }
            catch (Exception ex)
            {
                return ResponseModel<IEnumerable<Autor>>.Error($"Erro ao listar autores: {ex.Message}");
            }
        }
        
        public async Task<ResponseModel<Autor>> BuscarAutorPorIdAsync(int id)
        {
            try
            {
                var autor = await _autorRepository.GetByIdAsync(id);
                
                if (autor == null)
                    return ResponseModel<Autor>.NotFound($"Autor com ID {id} não encontrado.");
                
                return ResponseModel<Autor>.Success(autor, "Autor encontrado com sucesso.");
            }
            catch (Exception ex)
            {
                return ResponseModel<Autor>.Error($"Erro ao buscar autor: {ex.Message}");
            }
        }
        
        public async Task<ResponseModel<Autor>> BuscarAutorPorIdLivroAsync(int idLivro)
        {
            try
            {
                var autor = await _autorRepository.GetAutorByLivroIdAsync(idLivro);
                
                if (autor == null)
                    return ResponseModel<Autor>.NotFound($"Autor do livro com ID {idLivro} não encontrado.");
                
                return ResponseModel<Autor>.Success(autor, "Autor encontrado com sucesso.");
            }
            catch (Exception ex)
            {
                return ResponseModel<Autor>.Error($"Erro ao buscar autor do livro: {ex.Message}");
            }
        }
        
        public async Task<ResponseModel<Autor>> CriarAutorAsync(Autor autor)
        {
            try
            {
                // Verificar se já existe autor com mesmo nome
                var autorExistente = await _autorRepository.GetByNomeCompletoAsync(autor.Nome, autor.Sobrenome);
                if (autorExistente != null)
                    return ResponseModel<Autor>.Error("Já existe um autor com este nome e sobrenome.");
                
                var autorCriado = await _autorRepository.AddAsync(autor);
                return ResponseModel<Autor>.Success(autorCriado, "Autor criado com sucesso.");
            }
            catch (Exception ex)
            {
                return ResponseModel<Autor>.Error($"Erro ao criar autor: {ex.Message}");
            }
        }
        
        public async Task<ResponseModel<Autor>> EditarAutorAsync(int id, Autor autor)
        {
            try
            {
                var autorExistente = await _autorRepository.GetByIdAsync(id);
                if (autorExistente == null)
                    return ResponseModel<Autor>.NotFound($"Autor com ID {id} não encontrado.");
                
                // Verificar se já existe outro autor com mesmo nome
                var autorComMesmoNome = await _autorRepository.GetByNomeCompletoAsync(autor.Nome, autor.Sobrenome);
                if (autorComMesmoNome != null && autorComMesmoNome.Id != id)
                    return ResponseModel<Autor>.Error("Já existe outro autor com este nome e sobrenome.");
                
                autorExistente.AtualizarNome(autor.Nome, autor.Sobrenome);
                var autorAtualizado = await _autorRepository.UpdateAsync(autorExistente);
                
                return ResponseModel<Autor>.Success(autorAtualizado, "Autor atualizado com sucesso.");
            }
            catch (Exception ex)
            {
                return ResponseModel<Autor>.Error($"Erro ao editar autor: {ex.Message}");
            }
        }
        
        public async Task<ResponseModel<bool>> ExcluirAutorAsync(int id)
        {
            try
            {
                var autor = await _autorRepository.GetByIdAsync(id);
                if (autor == null)
                    return ResponseModel<bool>.NotFound($"Autor com ID {id} não encontrado.");
                
                if (autor.PossuiLivros)
                    return ResponseModel<bool>.Error("Não é possível excluir um autor que possui livros.");
                
                var resultado = await _autorRepository.DeleteAsync(id);
                return ResponseModel<bool>.Success(resultado, "Autor excluído com sucesso.");
            }
            catch (InvalidOperationException ex)
            {
                return ResponseModel<bool>.Error(ex.Message);
            }
            catch (Exception ex)
            {
                return ResponseModel<bool>.Error($"Erro ao excluir autor: {ex.Message}");
            }
        }
    }
}
