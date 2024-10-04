using CriandoApi8ParaTestar.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webApiTcc.Application.DTO.Request;
using webApiTcc.Application.DTO.Response;
using webApiTcc.Application.IServices;
using webApiTcc.Repository;

namespace webApiTcc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacoesController : ControllerBase
    {
        private readonly IAutenticacoesServices _autenticacoesServices;
        public AutenticacoesController(IAutenticacoesServices service)
        {
            _autenticacoesServices = service;
        }
        [HttpPost]
        public IActionResult Login([FromBody] AutenticacaoRequest request)
        {
            try
            {
                AutenticacaoResponse resposta = _autenticacoesServices.Autenticar(request);
                if (resposta == null)
                {
                    return Unauthorized();
                }
                return Ok(resposta);
            }
            catch (Exception ex)
            {
                var exception = new GravaLogExceptionRequest
                {
                    dataHora = DateTime.Now,
                    excecao = ex.Message,
                    referencia = $"Rota: Autenticacoes - Metodo: Login - Usuario: {request.Usuario}"
                };

                _autenticacoesServices.GravaLogException(exception);
                return BadRequest();
            }

        }
        [HttpPost("GravaLogException")]
        public IActionResult GravaLogException(GravaLogExceptionRequest request)
        {
            bool resposta = _autenticacoesServices.GravaLogException(request);
            if (resposta == true)
                return Ok();
            else
                return BadRequest();

        }
    }
}



