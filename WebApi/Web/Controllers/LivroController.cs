using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
using Domain.Models;
using Domain.Interfaces;

namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;
        
        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }
        
        /// <summary>
        /// Lista todos os livros com seus autores
        /// </summary>
        /// <returns>Lista de livros</returns>
        [HttpGet("ListarLivros")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<IEnumerable<Livro>>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResponseModel<IEnumerable<Livro>>>> ListarLivros()
        {
            var response = await _livroService.ListarLivrosAsync();
            
            if (!response.Status)
                return NotFound(response);
                
            return Ok(response);
        }
        
        /// <summary>
        /// Busca um livro por ID
        /// </summary>
        /// <param name="id">ID do livro</param>
        /// <returns>Livro encontrado</returns>
        [HttpGet("BuscarLivroPorId/{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<Livro>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResponseModel<Livro>>> BuscarLivroPorId(int id)
        {
            if (id <= 0)
                return BadRequest(ResponseModel<Livro>.Error("ID deve ser maior que zero."));
                
            var response = await _livroService.BuscarLivroPorIdAsync(id);
            
            if (!response.Status)
                return NotFound(response);
                
            return Ok(response);
        }
        
        /// <summary>
        /// Cria um novo livro
        /// </summary>
        /// <param name="livroDTO">Dados do livro</param>
        /// <returns>Livro criado</returns>
        [HttpPost("CriarLivro")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<Livro>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel<Livro>>> CriarLivro([FromBody] LivroDTO livroDTO)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                    
                return BadRequest(ResponseModel<Livro>.ValidationError("Dados inválidos", erros));
            }
            
            var livro = new Livro
            {
                Titulo = livroDTO.Titulo,
                AutorId = livroDTO.AutorId
            };
            
            var response = await _livroService.CriarLivroAsync(livro);
            
            if (!response.Status)
                return BadRequest(response);
                
            return Ok(response);
        }
        
        /// <summary>
        /// Edita um livro existente
        /// </summary>
        /// <param name="id">ID do livro</param>
        /// <param name="livroDTO">Novos dados do livro</param>
        /// <returns>Livro atualizado</returns>
        [HttpPut("EditarLivro/{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<Livro>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResponseModel<Livro>>> EditarLivro(int id, [FromBody] LivroUpdateDTO livroDTO)
        {
            if (id <= 0)
                return BadRequest(ResponseModel<Livro>.Error("ID deve ser maior que zero."));
                
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                    
                return BadRequest(ResponseModel<Livro>.ValidationError("Dados inválidos", erros));
            }
            
            var livro = new Livro
            {
                Titulo = livroDTO.Titulo,
                AutorId = livroDTO.AutorId
            };
            
            var response = await _livroService.EditarLivroAsync(id, livro);
            
            if (!response.Status)
            {
                if (response.Mensagem.Contains("não encontrado"))
                    return NotFound(response);
                    
                return BadRequest(response);
            }
                
            return Ok(response);
        }
        
        /// <summary>
        /// Exclui um livro
        /// </summary>
        /// <param name="id">ID do livro</param>
        /// <returns>Confirmação da exclusão</returns>
        [HttpDelete("ExcluirLivro/{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<bool>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResponseModel<bool>>> ExcluirLivro(int id)
        {
            if (id <= 0)
                return BadRequest(ResponseModel<bool>.Error("ID deve ser maior que zero."));
                
            var response = await _livroService.ExcluirLivroAsync(id);
            
            if (!response.Status)
            {
                if (response.Mensagem.Contains("não encontrado"))
                    return NotFound(response);
                    
                return BadRequest(response);
            }
                
            return Ok(response);
        }
    }
}
