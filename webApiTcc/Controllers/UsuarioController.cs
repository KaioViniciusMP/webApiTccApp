using Azure.Core;
using CriandoApi8ParaTestar.Application.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using webApiTcc.Application.DTO.Request;
using webApiTcc.Application.IServices;

namespace webApiTcc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAutenticacoesServices _autenticacoesServices;
        public UsuarioController(IUsuarioService service, IAutenticacoesServices autenticacoesServices)
        {
            _usuarioService = service;
            _autenticacoesServices = autenticacoesServices;
        }

        [HttpGet("BuscarUsuariosCadastrados")]
        public IActionResult BuscarUsuariosCadastrados()
        {
            try
            {
                var response = _usuarioService.BuscarUsuariosCadastrados();
                if (response == null)
                    return NotFound();
                else
                    return Ok(response);
            }
            catch (Exception ex)
            {
                var exception = new GravaLogExceptionRequest
                {
                    dataHora = DateTime.Now,
                    excecao = ex.Message,
                    referencia = $"Rota: Usuario - Metodo: BuscarUsuariosCadastrados"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }
        }

        [HttpPost]
        public IActionResult CadastrarUsuario([FromBody] UsuarioRequest request)
        {
            try
            {
                var response = _usuarioService.CadastrarUsuario(request);
                if (response.status)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                var exception = new GravaLogExceptionRequest
                {
                    dataHora = DateTime.Now,
                    excecao = ex.Message,
                    referencia = $"Rota: Usuario - Metodo: CadastrarUsuario - usuario: {request.usuario}"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("{codigoUsuario}")]
        public IActionResult EditarUsuario([FromBody] UsuarioRequest request, int codigoUsuario)
        {
            try
            {
                var response = _usuarioService.EditarUsuario(request, codigoUsuario);
                if (response.status)
                    return Ok(response);
                else
                    return BadRequest(response);
            }
            catch (Exception ex)
            {
                var exception = new GravaLogExceptionRequest
                {
                    dataHora = DateTime.Now,
                    excecao = ex.Message,
                    referencia = $"Rota: Usuario - Metodo: EditarUsuario - codigoUsuario: {codigoUsuario}"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }
        }
    }
}
