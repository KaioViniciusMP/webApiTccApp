using CriandoApi8ParaTestar.Application.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using webApiTcc.Application.IServices;

namespace webApiTcc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService service)
        {
            _usuarioService = service;
        }

        [HttpGet("BuscarUsuariosCadastrados")]
        public IActionResult BuscarUsuariosCadastrados()
        {
            var response = _usuarioService.BuscarUsuariosCadastrados();
            if (response == null)
                return NotFound();
            else
                return Ok(response);
        }

        [HttpPost]
        public IActionResult CadastrarUsuario([FromBody] UsuarioRequest request)
        {
            var response = _usuarioService.CadastrarUsuario(request);
            if (response.status)
                return Ok(response);
            else
                return BadRequest(response);
        }

        [HttpPut]

        [Route("{codigoUsuario}")]
        public IActionResult EditarUsuario([FromBody] UsuarioRequest request, int codigoUsuario)
        {
            var response = _usuarioService.EditarUsuario(request, codigoUsuario);
            if (response.status)
                return Ok(response);
            else
                return BadRequest(response);
        }
    }
}
