using Application.DTOs;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;
        
        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }
        
        /// <summary>
        /// Lista todos os autores
        /// </summary>
        /// <returns>Lista de autores</returns>
        [HttpGet("ListarAutores")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<IEnumerable<Autor>>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResponseModel<IEnumerable<Autor>>>> ListarAutores()
        {
            var response = await _autorService.ListarAutoresAsync();
            
            if (!response.Status)
                return NotFound(response);
                
            return Ok(response);
        }
        
        /// <summary>
        /// Busca um autor por ID
        /// </summary>
        /// <param name="id">ID do autor</param>
        /// <returns>Autor encontrado</returns>
        [HttpGet("BuscarAutorPorId/{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<Autor>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResponseModel<Autor>>> BuscarAutorPorId(int id)
        {
            if (id <= 0)
                return BadRequest(ResponseModel<Autor>.Error("ID deve ser maior que zero."));
                
            var response = await _autorService.BuscarAutorPorIdAsync(id);
            
            if (!response.Status)
                return NotFound(response);
                
            return Ok(response);
        }
        
        /// <summary>
        /// Busca o autor de um livro específico
        /// </summary>
        /// <param name="idLivro">ID do livro</param>
        /// <returns>Autor do livro</returns>
        [HttpGet("BuscarAutorPorIdLivro/{idLivro}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<Autor>))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResponseModel<Autor>>> BuscarAutorPorIdLivro(int idLivro)
        {
            if (idLivro <= 0)
                return BadRequest(ResponseModel<Autor>.Error("ID do livro deve ser maior que zero."));
                
            var response = await _autorService.BuscarAutorPorIdLivroAsync(idLivro);
            
            if (!response.Status)
                return NotFound(response);
                
            return Ok(response);
        }
        
        /// <summary>
        /// Cria um novo autor
        /// </summary>
        /// <param name="autorDTO">Dados do autor</param>
        /// <returns>Autor criado</returns>
        [HttpPost("CriarAutor")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<Autor>))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ResponseModel<Autor>>> CriarAutor([FromBody] AutorDTO autorDTO)
        {
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                    
                return BadRequest(ResponseModel<Autor>.ValidationError("Dados inválidos", erros));
            }
            
            var autor = new Autor
            {
                Nome = autorDTO.Nome,
                Sobrenome = autorDTO.Sobrenome
            };
            
            var response = await _autorService.CriarAutorAsync(autor);
            
            if (!response.Status)
                return BadRequest(response);
                
            return Ok(response);
        }
        
        /// <summary>
        /// Edita um autor existente
        /// </summary>
        /// <param name="id">ID do autor</param>
        /// <param name="autorDTO">Novos dados do autor</param>
        /// <returns>Autor atualizado</returns>
        [HttpPut("EditarAutor/{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<Autor>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResponseModel<Autor>>> EditarAutor(int id, [FromBody] AutorUpdateDTO autorDTO)
        {
            if (id <= 0)
                return BadRequest(ResponseModel<Autor>.Error("ID deve ser maior que zero."));
                
            if (!ModelState.IsValid)
            {
                var erros = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                    
                return BadRequest(ResponseModel<Autor>.ValidationError("Dados inválidos", erros));
            }
            
            var autor = new Autor
            {
                Nome = autorDTO.Nome,
                Sobrenome = autorDTO.Sobrenome
            };
            
            var response = await _autorService.EditarAutorAsync(id, autor);
            
            if (!response.Status)
            {
                if (response.Mensagem.Contains("não encontrado"))
                    return NotFound(response);
                    
                return BadRequest(response);
            }
                
            return Ok(response);
        }
        
        /// <summary>
        /// Exclui um autor
        /// </summary>
        /// <param name="id">ID do autor</param>
        /// <returns>Confirmação da exclusão</returns>
        [HttpDelete("ExcluirAutor/{id}")]
        [ProducesResponseType(200, Type = typeof(ResponseModel<bool>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ResponseModel<bool>>> ExcluirAutor(int id)
        {
            if (id <= 0)
                return BadRequest(ResponseModel<bool>.Error("ID deve ser maior que zero."));
                
            var response = await _autorService.ExcluirAutorAsync(id);
            
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
