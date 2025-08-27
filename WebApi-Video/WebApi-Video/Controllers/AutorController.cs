using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi_Video.Models;
using WebApi_Video.Services.Autor;

namespace WebApi_Video.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }
        [HttpGet("ListarAutores")]
        public async Task<ActionResult<ResponseModel<List<AutorModel>>>> ListarAutores()
        {
            var response = await _autorService.ListarAutores();
            if (!response.Status)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<AutorModel>>> BuscarAutorPorId(int id)
        {
            var result = await _autorService.BuscarAutorPorId(id);
            if (!result.Status)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
