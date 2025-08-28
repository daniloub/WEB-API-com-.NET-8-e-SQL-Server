using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Video.Dtos;
using WebApi_Video.Services.Livros;

namespace WebApi_Video.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livrosService;

        public LivroController(ILivroService livrosService)
        {
            _livrosService = livrosService;
        }

        [HttpGet("ListarLivros")]
        public async Task<ActionResult> ListarLivros()
        {
            var response = await _livrosService.ListarLivros();
            if (!response.Status)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("CriarLivro")]
        public async Task<ActionResult> CriarLivro([FromBody] LivroDTO livroDTO)
        {
            var response = await _livrosService.CriarLivro(livroDTO);
            if (!response.Status)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
